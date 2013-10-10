#include "stdafx.h"
#include "tcm_xml.h"
#include "tcm_translator.h"
#include <iostream>

namespace tcm
{
	using std::wstring;
	using std::cout;
	using std::endl;
	using namespace rapidxml;

	const int _ZoneCodes[] = {1078,1052,14337,15361,5121,3073,2049,11265,13313,12289,4097,6145,8193,16385,1025,
						10241,7169,9217,1067,1068,2092,1069,1059,1026,1027,2052,3076,5124,4100,1028,1050,1029,
						1030,1043,2067,3081,10249,4105,9225,16393,6153,8201,17417,5129,13321,18441,7177,11273,
						2057,1033,12297,1061,1065,1035,1080,1036,2060,3084,5132,4108,2108,1084,1031,3079,5127,
						4103,2055,1032,1037,1081,1038,1039,1057,1040,2064,1041,1042,1062,1063,1071,1086,2110,
						1082,1102,1044,2068,1045,2070,1046,1047,1048,2072,1049,2073,1103,3098,2074,1074,1060,
						1051,1070,3082,1034,11274,16394,13322,9226,5130,7178,12298,4106,18442,2058,19466,6154,
						10250,20490,15370,17418,14346,8202,1072,1089,1053,	2077,1097,1092,1054,1055,1073,1058,
						1056,2115,1091,1066,1076,1085,1077};

	const PCWSTR _LangNames[] = { L"af", L"sq", L"ar-AE", L"ar-BH", L"ar-DZ", L"ar-EG", L"ar-IQ", L"ar-JO", L"ar-KW",
						L"ar-LB", L"ar-LY", L"ar-MA", L"ar-OM", L"ar-QA", L"ar-SA", L"ar-SY", L"ar-TN", L"ar-YE",
						L"hy", L"az-AZ-l", L"az-AZ-c", L"eu", L"be", L"bg", L"ca", L"zh-CN", L"zh-HK", L"zh-MO",
						L"zh-SG", L"zh-TW", L"hr", L"cs", L"da", L"nl-NL", L"nl-BE", L"en-AU", L"en-BZ", L"en_CA",
						L"en-CB", L"en-IN", L"en-IE", L"en-JM", L"en-MY", L"en-NZ", L"en-PH", L"en-SG", L"en-ZA",
						L"en-TT", L"en-GB", L"en-US", L"en-ZW", L"et", L"fa", L"fi", L"fo", L"fr-FR", L"fr-BE",
						L"fr-CA", L"fr-LU", L"fr-CH", L"gd-IE", L"gd", L"de-DE", L"de-AT", L"de-LI", L"de-LU",
						L"de-CH", L"el", L"he", L"hi", L"hu", L"is", L"id", L"it-IT", L"it-CH", L"ja", L"ko",
						L"lv", L"lt", L"mk", L"ms-MY", L"ms-BN", L"mt", L"mr", L"nb-NO", L"nn-NO", L"pl", L"pt-PT",
						L"pt-BR", L"rm", L"ro", L"ro-MO", L"ru", L"ru-MO", L"sa", L"sr-SP-c", L"sr-SP-l", L"tn",
						L"sl", L"sk", L"sb", L"es-ES", L"es-AR", L"es-BO", L"es-CL", L"es-CO", L"es-CR", L"es-DO",
						L"es-EC", L"es-GT", L"es-HN", L"es-MX", L"es-NI", L"es-PA", L"es-PE", L"es-PR", L"es-PY",
						L"es-SV", L"es-UY", L"es-VE", L"st", L"sw", L"sv-SE", L"sv-FI", L"ta", L"tt", L"th",
						L"tr", L"ts", L"uk", L"ur", L"uz-UZ-c", L"uz-UZ-l", L"vi", L"xh", L"yi", L"zu"};


	void SeeAlsoLangCode()
	{
		system("start http://zh.wikipedia.org/wiki/%E5%8C%BA%E5%9F%9F%E8%AE%BE%E7%BD%AE");
	}

	int GetZoneCode(int lc)
	{
		if(lc < 0 || lc > 137) return 0;
		return _ZoneCodes[lc];
	}

	PCWSTR GetLangName(int lc)
	{
		if(lc < 0 || lc > 137) return NULL;
		return _LangNames[lc];
	}

	Translator::Translator()
	{
		_Dict = NULL;
	}

	Translator::~Translator()
	{
		if(_Dir != NULL) delete _Dir;
		if(_Dict != NULL) delete _Dict;
	}

	void Translator::SetDictDir(PCWSTR dir)
	{
		_Dir = dir;
	}

	void Translator::LoadLangPack(int lc)
	{
		PCWSTR name = GetLangName(lc);
		if(name == NULL) return;
		wstring path = _Dir;
		path += name;
		path += L".xml";
		std::locale::global(std::locale(""));
		rapidxml::file<> xfile(WcToMb(path.c_str()));
		PCSTR data = CopyStrA(xfile.data());
		if(data)
		{
			_Dict->Clear();
			xml_document<> xdoc;
			xdoc.parse<0>(const_cast<PSTR>(data));
			xml_node<>* xe = (xml_node<>*)xml::Path(&xdoc,2,"root","item");
			while (xe)
			{
				PCWSTR tmp = xml::ValueW(xe->first_attribute("key"));
				_Dict->Add(tmp, xml::ValueW(xe));
				xe = xe->next_sibling();
			}
			delete data;
		}
	}

	PCWSTR Translator::GetText(PCWSTR key)
	{
		PCWSTR tmp = _Dict->Find(key);
		return tmp;
	}
}