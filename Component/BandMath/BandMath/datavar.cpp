#include "stdafx.h"
#include "datavar.h"
#include "gdal_priv.h"
#include "cpl_conv.h"
#include "tcm_xml.h"

DataDef::DataDef()
{
	_CacheLen = 1024 / sizeof(double) * 32; //32KB
	_CountIn = 0;
	_DataVarsIn = NULL;
	_DataVarOut = NULL;
}

DataDef::~DataDef()
{
	for(int i=0;i<_CountIn;i++)
	{
		if(_DataVarsIn[i] != NULL) delete _DataVarsIn[i];
	}
	if(_DataVarOut != NULL) delete _DataVarOut;
}

PDataVar DataDef::GetDataVar(PCSTR name)
{
	for(int i=0;i<_CountIn;i++)
		if(strcmp(_DataVarsIn[i]->_Name,name) == 0) 
			return _DataVarsIn[i];
	return NULL;
}

//打开所有输入，尝试打开输出，不获取输出波段
void DataDef::BeginAction()
{
	GDALAllRegister();
	//CPLSetConfigOption("GDAL_FILENAME_IS_UTF8","NO");
	_DataVarOut->_GDALDataset = (GDALDataset*)GDALOpen(_DataVarOut->_File, GA_Update);
	for(int i=0;i<_CountIn;i++)
	{
		PDataVar ds = _DataVarsIn[i];
		ds->_GDALDataset = (GDALDataset*)GDALOpen(ds->_File, GA_ReadOnly);
		ds->_GDALBand = ((GDALDataset*)(ds->_GDALDataset))->GetRasterBand(ds->_BandId);
		if(ds->_GDALDataset != NULL)
		{
			ds->_Type = ((GDALRasterBand*)(ds->_GDALBand))->GetRasterDataType();
			ds->_Data = new BYTE[_CacheLen * GDALGetDataTypeSize((GDALDataType)ds->_Type)];
		}
	}
}

void DataDef::EndAction()
{
	GDALClose(_DataVarOut->_GDALDataset);
	for(int i=0;i<_CountIn;i++)
	{
		if(_DataVarsIn[i]->_GDALDataset != NULL)
			GDALClose(_DataVarsIn[i]->_GDALDataset);
	}
}

DataVar::DataVar()
{
	_GDALDataset = NULL;
	_GDALBand = NULL;
	_Data = NULL;
	_File = NULL;
	_Name = NULL;
	_BandId = 1;
}

DataSet::~DataSet()
{
	if(_Name != NULL) delete _Name;
	if(_File != NULL) delete _File;
	if(_Data != NULL) delete [] _Data;
}