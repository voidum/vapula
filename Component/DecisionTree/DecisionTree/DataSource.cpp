#include "stdafx.h"
#include "DataSource.h"
#include "gdal_priv.h"
#include "cpl_conv.h"

DataSource::DataSource()
{
	_CacheLen = 1024 / sizeof(double) * 32; //32KB
	_CountIn = 0;
	_DataSetsIn = NULL;
	_DataSetOut = NULL;
}

DataSource::~DataSource()
{
	for(int i=0;i<_CountIn;i++)
	{
		if(_DataSetsIn[i] != NULL)
		{
			delete _DataSetsIn[i];
			_DataSetsIn[i] = NULL;
		}
	}
	if(_DataSetOut != NULL)
	{
		delete _DataSetOut;
		_DataSetOut = NULL;
	}
}

PDataSet DataSource::GetDataSet(PCSTR name)
{
	for(int i=0;i<_CountIn;i++)
		if(strcmp(_DataSetsIn[i]->_Name,name) == 0) 
			return _DataSetsIn[i];
	return NULL;
}

//打开所有输入，尝试打开输出，不获取输出波段
void DataSource::BeginAction()
{
	GDALAllRegister();
	_DataSetOut->_GDALDataset = (GDALDataset*)GDALOpen(_DataSetOut->_File, GA_Update);
	for(int i=0;i<_CountIn;i++)
	{
		PDataSet ds = _DataSetsIn[i];
		ds->_GDALDataset = (GDALDataset*)GDALOpen(ds->_File, GA_ReadOnly);
		ds->_GDALBand = ((GDALDataset*)(ds->_GDALDataset))->GetRasterBand(ds->_BandId);
		if(ds->_GDALDataset != NULL)
		{
			ds->_Type = ((GDALRasterBand*)(ds->_GDALBand))->GetRasterDataType();
			ds->_Data = new BYTE[_CacheLen * GDALGetDataTypeSize((GDALDataType)ds->_Type)];
		}
	}
}

void DataSource::EndAction()
{
	if(_DataSetOut->_GDALDataset != NULL)
		GDALClose(_DataSetOut->_GDALDataset);
	for(int i=0;i<_CountIn;i++)
	{
		if(_DataSetsIn[i]->_GDALDataset != NULL)
			GDALClose(_DataSetsIn[i]->_GDALDataset);
	}
}

DataSet::DataSet()
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