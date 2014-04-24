#pragma once

#pragma warning(disable:4100)

#include "vf_base.h"
#include "vf_record.h"

namespace vapula
{
	//dataset
	class VAPULA_API Dataset
	{
	private:
		int32 _Total; //record total
		PRecord* _Records; //records

	private:
		Dataset();
	public:
		~Dataset();

	public:
		//parse dataset from XML string
		//need node <schema>
		static Dataset* Parse(pcstr xml);

	private:
		bool _AssertId(int id, Dataset* ds = null);

	public:
		//get record total
		int32 GetTotal();

	public:
		//get record by id
		PRecord operator [] (int id);

	public:
		//clear dataset
		void Zero();

		//copy dataset
		Dataset* Copy();
	};
}