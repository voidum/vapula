#include <iostream>
#include "tcmmain.h"
#include "tcm_xml.h"
#include "tcm_context.h"
#include "tcm_envelope.h"
#include "rapidxml/rapidxml.hpp"
#include "rapidxml/rapidxml_utils.hpp"
#include "rapidxml/rapidxml_print.hpp"
#include <stack>

using namespace std;
using namespace tcm;
using namespace rapidxml;

int main()
{
	rapidxml::file<> xfile("E:\\Projects\\TCM\\tcm_components\\DecisionTree\\OutDir\\debug-vc10\\DecisionTree.tcm.xml");
	PCSTR content = CopyStrA(xfile.data());
	xml_document<> xdoc;
	xdoc.parse<0>(const_cast<PSTR>(content));
	xml_node<>* xnode = (xml_node<>*)GetXElemByPath(&xdoc,5,"root","component","functions","function","params");
	string xtext;
	rapidxml::print(back_inserter(xtext),*xnode,0);
	//PCSTR str_copy = FixEncoding(CopyStrA(xtext.c_str()));
	PCSTR str_copy = CopyStrA(xtext.c_str());
	//PCSTR str_copy = CopyStrA(xtext.c_str());
	Envelope* env = Envelope::Parse(str_copy);
	Context* ctx = new Context();

	env->Write(0, L"E:\\Projects\\TCM\\tcm_components\\DecisionTree\\OutDir\\debug-vc10\\20130409161749-dt.xml");
	env->Write(1, L"E:\\Projects\\TCM\\tcm_components\\DecisionTree\\OutDir\\debug-vc10\\20130409161749-ds.xml");

	Run(0,env,ctx);
	return 0;
}