#include "stdafx.h"
#include "decitree.h"
#include "tcm_xml.h"

#include "gdal_priv.h"
#include "cpl_conv.h"

extern "C"
{
#include "lua.h"
#include "lauxlib.h"  
#include "lualib.h"
};

using namespace tcm;
using std::cout;
using std::endl;
using std::string;
using rapidxml::xml_document;
using rapidxml::xml_node;

NodeBase* GetNodeById(vector<NodeBase*> nodes,int id)
{
	for(vector<NodeBase*>::iterator iter=nodes.begin();iter!=nodes.end();iter++)
	{
		NodeBase* node = *iter;
		if(node->_Id == id) return node;
	}
	return NULL;
}

NodeBase* BuildDeciTree(PCWSTR file,vector<NodeBase*>* tree)
{
	rapidxml::file<> xfile(WcToMb(file));
	PCSTR content = CopyStrA(xfile.data());
	xml_document<> xdoc;
	xdoc.parse<0>(const_cast<PSTR>(content));
	xml_node<>* xnode = (xml_node<>*)GetXElemByPath(&xdoc,3,"root","nodes","node");
	while(xnode)
	{
		NodeBase* node = NodeBase::Parse(xnode);
		tree->push_back(node);
		xnode = xnode->next_sibling();
	}

	//构建树结构
	int clsid = 0;
	NodeBase* noderoot = NULL;
	xnode = (xml_node<>*)GetXElemByPath(&xdoc,3,"root","nodes","node");
	while(xnode)
	{
		PCSTR tmpa = GetXBaseValueA(xnode->first_attribute("id"));
		int tmpv = atoi(tmpa);
		delete tmpa;
	
		NodeBase* node = GetNodeById(*tree,tmpv);
		if(node == NULL) { cout<<"error"<<endl;exit(TCM_RETURNCODE_ERROR); }
		if(node->_Type == NT_Judge)
		{
			NodeJudge* nj = dynamic_cast<NodeJudge*>(node);
			if(tmpv == 0) noderoot = nj;

			tmpa = GetXBaseValueA(xnode->first_attribute("nl"));
			tmpv = atoi(tmpa);
			delete tmpa;
			nj->_NodeYes = GetNodeById(*tree,tmpv);

			tmpa = GetXBaseValueA(xnode->first_attribute("nr"));
			tmpv = atoi(tmpa);
			delete tmpa;
			nj->_NodeNo = GetNodeById(*tree,tmpv);
		}
		else
		{
			NodeClass* nc = dynamic_cast<NodeClass*>(node);
			nc->_ClassId = clsid++;
		}
		xnode = xnode->next_sibling();
	}
	delete content;
	return noderoot;
}

DataSource* LoadDataSource(PCWSTR file)
{
	rapidxml::file<> xfile(WcToMb(file));
	PCSTR content = CopyStrA(xfile.data());
	xml_document<> xdoc;
	xdoc.parse<0>(const_cast<PSTR>(content));
	
	DataSource* dsrc = new DataSource();
	vector<DataSet*> vdsin;
	
	xml_node<>* xnode = (xml_node<>*)GetXElemByPath(&xdoc,3,"root","mappings","mapping");
	while(xnode)
	{
		PDataSet ds = new DataSet();
		//ds->_Name = FixEncoding(GetXBaseValue(xnode->first_node("name")));
		//ds->_File = FixEncoding(GetXBaseValue(xnode->first_node("file")));
		ds->_Name = GetXBaseValueA(xnode->first_node("name"));
		ds->_File = GetXBaseValueA(xnode->first_node("file"));
		
		PCSTR tmpa = GetXBaseValueA(xnode->first_node("band"));
		int tmpv = atoi(tmpa);
		delete tmpa;
		ds->_BandId = tmpv;
		
		vdsin.push_back(ds);
		xnode = xnode->next_sibling();
	}
	dsrc->_CountIn = vdsin.size();
	int i=0;
	dsrc->_DataSetsIn = new PDataSet[dsrc->_CountIn];
	for (vector<DataSet*>::iterator iter=vdsin.begin();iter!=vdsin.end();iter++)
	{
		dsrc->_DataSetsIn[i] = *iter;
		(*iter)->_Id = i++;
	}

	xnode = (xml_node<>*)GetXElemByPath(&xdoc,2,"root","output");
	//PCSTR tmpa = FixEncoding(GetXBaseValue(xnode->first_node("file")));
	PCSTR tmpa = GetXBaseValueA(xnode->first_node("file"));
	string tmps = tmpa;
	delete tmpa;
	tmpa = GetXBaseValueA(xnode->first_node("format"));
	int tmpv = atoi(tmpa);
	delete tmpa;
	switch(tmpv)
	{
	case 0:
		tmps += ".tif";
		break;
	case 1:
		tmps += ".img";
		break;
	}

	PDataSet ds = new DataSet();
	ds->_BandId = 1;
	ds->_Name = CopyStrA("out");
	ds->_File = CopyStrA(tmps.c_str());
	dsrc->_DataSetOut = ds;

	delete content;
	return dsrc;
}

void LuaSetData(LPVOID luastate, PCSTR key, double value)
{
	lua_State* L = (lua_State*)luastate;
	lua_getglobal(L, "dv");
	lua_pushstring(L, key);
	lua_pushnumber(L, value);
	lua_settable(L, -3);
}

LPVOID PreCompile(vector<NodeBase*>* tree,DataSource* ds)
{
	lua_State* L = luaL_newstate();
	luaL_openlibs(L);

	string tmps = "";
	for(vector<NodeBase*>::iterator iter=tree->begin();iter!=tree->end();iter++)
	{
		NodeJudge* nj = dynamic_cast<NodeJudge*>(*iter);
		if(nj != NULL)
		{
			PCSTR tmpa = nj->Compile();
			tmps += tmpa;
			delete tmpa;
		};
	}

	luaL_loadstring(L,CopyStrA(tmps.c_str()));
	if(lua_pcall(L,0,0,0))
	{
		lua_close(L);
		return NULL;
	}
	return L;
}

void ExecDeciTree(LPVOID luastate, NodeBase* treeroot,DataSource* dsrc,Context* ctx)
{
	cout<<"<DTC>read basic info from first data";
	PDataSet ds = dsrc->_DataSetsIn[0];
	GDALDataset* Gds = (GDALDataset*)ds->_GDALDataset;
	if(Gds == NULL)
	{
		cout<<" >> <Fail>"<<endl;
		return;
	}
	int size_x = Gds->GetRasterXSize();
	int size_y = Gds->GetRasterYSize();
	cout<<" >> <OK>"<<endl;
	
	cout<<"<DTC>get driver and create output";
	GDALDriver* driver = GetGDALDriverManager()->GetDriverByName("GTiff");
	ds = dsrc->_DataSetOut;
	if(ds->_GDALDataset == NULL)
		ds->_GDALDataset = driver->Create(ds->_File,size_x,size_y,1,GDT_Byte,NULL);
	ds->_GDALBand = ((GDALDataset*)(ds->_GDALDataset))->GetRasterBand(1);
	for (int i=0;i<dsrc->_CountIn;i++)
	{
		ds = dsrc->_DataSetsIn[i];
		if(ds->_GDALBand == NULL)
			ds->_GDALBand = ((GDALDataset*)(ds->_GDALDataset))->GetRasterBand(ds->_BandId); 
	}
	cout<<" >> <OK>"<<endl;

	LARGE_INTEGER freq,time1,time2;
	QueryPerformanceFrequency(&freq);

	cout<<"<DTC>executing classify";
	lua_State* L = (lua_State*)luastate;
	lua_newtable(L);
	lua_setglobal(L, "dv");

	BYTE* data_result = new BYTE[dsrc->_CacheLen];
	memset(data_result,0,dsrc->_CacheLen);
	int ReadStep = size_x / dsrc->_CacheLen;
	if(size_x % dsrc->_CacheLen > 0) ReadStep++;

	ctx->SetProgress(0);
	QueryPerformanceCounter(&time1);
	for (int iy=0;iy<size_y;iy++)
	{
		int xoffset = 0;
		for (int istep=0;istep<ReadStep;istep++)
		{
			int xtoread = dsrc->_CacheLen;
			if(xoffset + xtoread > size_x) xtoread = size_x - xoffset;
			for (int i=0;i<dsrc->_CountIn;i++)
			{
				ds = dsrc->_DataSetsIn[i];
				GDALRasterBand* tmpGband = (GDALRasterBand*)(ds->_GDALBand);
				tmpGband->RasterIO(GF_Read,xoffset,iy,xtoread,1,ds->_Data,xtoread,1,(GDALDataType)(ds->_Type),0,0);
			}
			for(int ix=0;ix<xtoread;ix++)
			{
				RecombineData(dsrc->_DataSetsIn,luastate,dsrc->_CountIn,ix);
				data_result[ix] = Classify(treeroot,luastate);
			}
			((GDALRasterBand*)(dsrc->_DataSetOut->_GDALBand))->RasterIO(GF_Write, xoffset, iy, xtoread, 1, data_result, xtoread, 1, GDT_Byte, 0, 0);
			xoffset += xtoread;
		}
		ctx->SetProgress(iy * 100.0 / size_y);
		//system("cls");
		//cout<<"progress:"<<iy*100.0/size_y<<"%"<<endl;
	}
	QueryPerformanceCounter(&time2);
	ctx->SetProgress(100);
	cout<<" >> <OK>"<<endl;
	cout<<" time : "<<((time2.QuadPart - time1.QuadPart) / (double)(freq.QuadPart))<<"(s)"<<endl;

	delete [] data_result;
	lua_close(L);
}

void OutputHeader(vector<NodeBase*>* tree,DataSource* ds)
{
	GDALDataset* pds = (GDALDataset*)(ds->_DataSetOut->_GDALDataset);
	GDALRasterBand* pband = pds->GetRasterBand(1);
	string content = "ENVI";
	content += "\nsamples = ";
	content += ValueToStrA(pband->GetXSize());
	content += "\nlines = ";
	content += ValueToStrA(pband->GetYSize());
	content += "\nbands = 1\nfile type = ENVI Classification\ndata type = 1\ninterleave = bsq\nsensor type = Unknown\nheader offset = ";
	long long count = pband->GetXSize() * pband->GetYSize();

	GDALClose(pds);
	ds->_DataSetOut->_GDALDataset = NULL;

	HANDLE hfile = CreateFile(MbToWc(ds->_DataSetOut->_File,CP_UTF8),GENERIC_READ,FILE_SHARE_READ,0,OPEN_EXISTING,0,0);
	if (hfile == INVALID_HANDLE_VALUE) 
	{
		cout<<"Win32 ERROR:"<<GetLastError()<<endl;
		return;
	}
	LARGE_INTEGER filesize;
	GetFileSizeEx(hfile,&filesize);
	CloseHandle(hfile);
	int headeroffset = (count < filesize.QuadPart) ? (filesize.QuadPart - count) : 0;
	content += ValueToStrA(headeroffset);

	vector<NodeClass*> ncs;
	for (vector<NodeBase*>::iterator iter=tree->begin();iter!=tree->end();iter++)
	{
		NodeBase* node = *iter;
		if(node->_Type == NT_Class) ncs.push_back((NodeClass*)node);
	}
	content += "\nclasses = ";
	content += ValueToStrA(ncs.size());
	content += "\nclass lookup = {\n";
	for (vector<NodeClass*>::iterator iter=ncs.begin();iter!=ncs.end();iter++)
	{
		NodeClass* node = *iter;
		content += ValueToStrA((int)(node->_ColorR));
		content += ",";
		content += ValueToStrA((int)(node->_ColorG));
		content += ",";
		content += ValueToStrA((int)(node->_ColorB));
		content += ",";
	}
	content[content.size()-1] = '}';
	content += "\nclass names = {\n";
	for (vector<NodeClass*>::iterator iter=ncs.begin();iter!=ncs.end();iter++)
	{
		NodeClass* node = *iter;
		//TODO: _Name可能是UTF8编码，会有问题
		content += node->_Name; 
		content += ",";
	}
	content[content.size()-1] = '}';
	content += "\nbyte order = 0";

	string file = FixEncoding(ds->_DataSetOut->_File);
	file = file.substr(0,file.size()-3);
	file += "hdr";
	std::ofstream fout(file.c_str());
	fout<<content.c_str()<<endl;
}

void Clear(vector<NodeBase*>* tree)
{
	for (vector<NodeBase*>::iterator iter=tree->begin();iter!=tree->end();iter++)
	{
		NodeBase* node = *iter;
		if(node != NULL) delete node;
	}
}

BYTE Classify(NodeBase* treeroot,LPVOID luastate)
{
	NodeBase* node = treeroot;
	while(node->_Type == NT_Judge)
	{
		NodeJudge* nj = (NodeJudge*)node;
		int ret = nj->Test(luastate);
		node = ret > 0 ? nj->_NodeYes : nj->_NodeNo;
	}
	NodeClass* nc = (NodeClass*)(node);
	return nc->_ClassId;
}

void RecombineData(PDataSet* dsrc,LPVOID luastate,int total,int index)
{
	lua_State* L = (lua_State*)luastate;
	double tmp;
	for(int i=0;i<total;i++)
	{
		switch(dsrc[i]->_Type)
		{
			case GDT_Byte:
				tmp = ((BYTE*)(dsrc[i]->_Data))[index];
				break;
			case GDT_Int16:
				tmp = ((short*)(dsrc[i]->_Data))[index];
				break;
			case GDT_Int32:
				tmp = ((int*)(dsrc[i]->_Data))[index];
				break;
			case GDT_UInt16:
				tmp = ((USHORT*)(dsrc[i]->_Data))[index];
				break;
			case GDT_UInt32:
				tmp = ((UINT*)(dsrc[i]->_Data))[index];
				break;
			case GDT_Float32:
				tmp = ((float*)(dsrc[i]->_Data))[index];
				break;
			case GDT_Float64:
				tmp = ((double*)(dsrc[i]->_Data))[index];
				break;
		}
		LuaSetData(luastate,dsrc[i]->_Name,tmp);
	}
}