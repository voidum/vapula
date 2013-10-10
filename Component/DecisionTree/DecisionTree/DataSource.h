#pragma once

#include "tcm_base.h"
#include "tcm_xml.h"

class DataSet;

typedef DataSet* PDataSet;

class DataSource
{
public:
	DataSource();
	~DataSource();
public:
	int _CacheLen;
	int _CountIn;
	PDataSet* _DataSetsIn;
	PDataSet _DataSetOut;
public:
	PDataSet GetDataSet(PCSTR name);
public:
	void BeginAction();
	void EndAction();
};

class DataSet
{
public:
	DataSet();
	~DataSet();
public:
	static PDataSet Parse(rapidxml::xml_node<>* xml);
public:
	LPVOID _GDALDataset;
	LPVOID _GDALBand;
public:
	int _Id;
	int _Type;
	PCSTR _Name;
	PCSTR _File;
	LPVOID _Data;
public:
	int _BandId;
};