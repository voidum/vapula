#include "stdafx.h"
#include "tcmmain.h"
#include "bandmath.h"

UINT Run(int function, Envelope* envelope, Context* context)
{
	if(function != 0) return TCM_RETURNCODE_NOFUNCTION;

	PCSTR expr = WcToMb(envelope->Read<PCWSTR>(0));
	PCSTR file_datadef = WcToMb(envelope->Read<PCWSTR>(1));
	
	DataDef* datadef = DataDef::Parse(file_datadef);
	datadef->BeginAction();
	ExecBandMath(expr, datadef, context);
	datadef->EndAction();

	delete datadef;
	return TCM_RETURNCODE_NORMAL;
}