#include "vf_dataset.h"
#include "vf_xml.h"

namespace vapula
{
	Dataset::Dataset()
	{
		_Records = null;
	}

	Dataset::~Dataset()
	{
		Zero();
		if(_Records == null)
			return;
		for (int i = 0; i < _Total; i++)
			if(_Records[i] != null)
				delete _Records[i];
		delete _Records;
	}

	Dataset* Dataset::Parse(pcstr xml)
	{
		XML* xobj = XML::Parse(xml);
		Scoped autop_xml(xobj);
		if (xobj == null)
			return null;
		raw xdoc = xobj->GetEntity();
		raw xe = XML::XElem(xdoc, "schema");
		return Parse(xe);
	}

	Dataset* Dataset::Parse(raw xml)
	{
		list<int> ids;
		list<PRecord> recs;
		raw xe = XML::XElem(xml, "field");
		while(xe != null)
		{
			PRecord rec = Record::Parse(xe);
			int id = XML::ValInt(XML::XAttr(xe, "id"));
			ids.push_back(id);
			recs.push_back(rec);
			xe = XML::Next(xe);
		}

		Dataset* ds = new Dataset();
		ds->_Records = new PRecord[recs.size()];
		ds->_Total = recs.size();

		list<int>::iterator i1 = ids.begin();
		list<PRecord>::iterator i2 = recs.begin();
		for(;;)
		{
			if(i1 == ids.end())
				break;
			int id = *i1;
			PRecord rec = *i2;
			ds->_Records[id - 1] = rec;
			i1++;
			i2++;
		}
		return ds;
	}

	PRecord Dataset::GetRecord(int id)
	{
		if (id < 1 && id > _Total)
			return null;
		return _Records[id - 1];
	}

	PRecord Dataset::operator[](int id)
	{
		if (id < 1 && id > _Total)
			return null;
		return _Records[id - 1];
	}

	int32 Dataset::GetTotal()
	{
		return _Total;
	}

	void Dataset::Zero()
	{
		for(int i = 0; i < _Total; i++)
		{
			if(_Records[i] != null)
				_Records[i]->Zero();
		}
	}

	Dataset* Dataset::Copy()
	{
		Dataset* ds = new Dataset();
		ds->_Total = _Total;
		ds->_Records = new PRecord[_Total];
		for(int i = 0; i < _Total; i++)
			ds->_Records[i] = _Records[i]->Copy();
		return ds;
	}
}