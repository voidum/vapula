#pragma once

class DataVar;
typedef DataVar* PDataVar;

class DataDef
{
public:
	DataDef();
	~DataDef();
public:
	static DataDef* Parse(PCSTR xmlfile);
public:
	int _CacheLen;
	int _CountIn;
	PDataVar* _DataVarsIn;
	PDataVar _DataVarOut;
public:
	PDataVar GetDataVar(PCSTR name);
public:
	void BeginAction();
	void EndAction();
};

class DataVar
{
public:
	PCSTR _Name;
	PCSTR _File;
	LPVOID _Data;
	bool _AsBand;
	int _BandTotal;
	int* _SpectralSubset;
	int* _SpatialSubset;
	LPVOID _GDALDataset;
	LPVOID _GDALBand;
public:
	static PDataVar Parse(LPVOID xml);
};