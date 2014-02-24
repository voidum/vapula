#pragma once

#pragma warning(disable:4100)

#include "vf_base.h"
#include "vf_variable.h"

namespace vapula
{
	//envelope for data
	class VAPULA_API Envelope
	{
	private:
		int32 _Total; //param total
		PVar* _Vars; //variables

	private:
		Envelope();
	public:
		~Envelope();

	public:
		//parse envelope from XML string
		//need node <params>
		static Envelope* Parse(pcstr xml);

	private:
		bool _AssertId(int id, Envelope* env = null);

	public:
		//get param total
		int32 GetTotal();

	public:
		//zero envelope
		void Zero();

		//copy envelope
		Envelope* Copy();

	public:
		//deliver envelope
		void Deliver(Envelope* who, int from, int to);

		//deliver envelope with auto-cast
		void CastDeliver(Envelope* who, int from, int to);
	};
}