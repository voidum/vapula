#include "vf_translator.h"
#include "vf_xml.h"
#include "rapidxml/rapidxml.hpp"

namespace vapula
{
	using std::cout;
	using std::endl;
	using namespace rapidxml;

	const int _ZoneCodes[] = {
		1078,1052,14337,15361,5121,3073,2049,11265,13313,
		12289,4097,6145,8193,16385,1025,10241,7169,9217,
		1067,1068,2092,1069,1059,1026,1027,2052,3076,5124,
		4100,1028,1050,1029,1030,1043,2067,3081,10249,4105,
		9225,16393,6153,8201,17417,5129,13321,18441,7177,
		11273,2057,1033,12297,1061,1065,1035,1080,1036,2060,
		3084,5132,4108,2108,1084,1031,3079,5127,4103,2055,
		1032,1037,1081,1038,1039,1057,1040,2064,1041,1042,
		1062,1063,1071,1086,2110,1082,1102,1044,2068,1045,
		2070,1046,1047,1048,2072,1049,2073,1103,3098,2074,
		1074,1060,1051,1070,3082,1034,11274,16394,13322,
		9226,5130,7178,12298,4106,18442,2058,19466,6154,
		10250,20490,15370,17418,14346,8202,1072,1089,1053,	
		2077,1097,1092,1054,1055,1073,1058,1056,2115,1091,
		1066,1076,1085,1077};

	const cstr8 _LangNames[] = { 
		"af", "sq", "ar-AE", "ar-BH", "ar-DZ", 
		"ar-EG", "ar-IQ", "ar-JO", "ar-KW",
		"ar-LB", "ar-LY", "ar-MA", "ar-OM", 
		"ar-QA", "ar-SA", "ar-SY", "ar-TN", "ar-YE",
		"hy", "az-AZ-l", "az-AZ-c", "eu", "be", 
		"bg", "ca", "zh-CN", "zh-HK", "zh-MO",
		"zh-SG", "zh-TW", "hr", "cs", "da", 
		"nl-N", "nl-BE", "en-AU", "en-BZ", "en_CA",
		"en-CB", "en-IN", "en-IE", "en-JM", "en-MY",
		"en-NZ", "en-PH", "en-SG", "en-ZA", "en-TT",
		"en-GB", "en-US", "en-ZW", "et", "fa", "fi",
		"fo", "fr-FR", "fr-BE", "fr-CA", "fr-LU", 
		"fr-CH", "gd-IE", "gd", "de-DE", "de-AT",
		"de-LI", "de-LU", "de-CH", "el", "he", "hi",
		"hu", "is", "id", "it-IT", "it-CH", "ja",
		"ko", "lv", "lt", "mk", "ms-MY", "ms-BN",
		"mt", "mr", "nb-NO", "nn-NO", "pl", "pt-PT",
		"pt-BR", "rm", "ro", "ro-MO", "ru", "ru-MO",
		"sa", "sr-SP-c", "sr-SP-l", "tn", "sl", "sk",
		"sb", "es-ES", "es-AR", "es-BO", "es-C", 
		"es-CO", "es-CR", "es-DO", "es-EC", "es-GT", 
		"es-HN", "es-MX", "es-NI", "es-PA", "es-PE", 
		"es-PR", "es-PY", "es-SV", "es-UY", "es-VE", 
		"st", "sw", "sv-SE", "sv-FI", "ta", "tt", "th",
		"tr", "ts", "uk", "ur", "uz-UZ-c", "uz-UZ-l", 
		"vi", "xh", "yi", "zu"};

	void SeeAlsoLangCode()
	{
		system("start http://zh.wikipedia.org/wiki/%E5%8C%BA%E5%9F%9F%E8%AE%BE%E7%BD%AE");
	}

	int GetZoneCode(int lc)
	{
		if(lc < 0 || lc > 137) return 0;
		return _ZoneCodes[lc];
	}

	cstr8 GetLangName(int lc)
	{
		if(lc < 0 || lc > 137) return null;
		return _LangNames[lc];
	}

	Translator::Translator()
	{
		_Dict = null;
	}

	Translator::~Translator()
	{
		if(_Dir != null) delete _Dir;
		if(_Dict != null) delete _Dict;
	}

	void Translator::SetDictDir(cstr8 dir)
	{
		_Dir = dir;
	}

	void Translator::LoadLangPack(int lc)
	{
		cstr8 name = GetLangName(lc);
		if(name == null) 
			return;
		string path = _Dir;
		path += name;
		path += ".vapula.langpack";
		cstr8 data = null;
		xml_document<>* xdoc = 
			(xml_document<>*)xml::Load(path.c_str(), data);
		_Dict->Clear();
		xml_node<>* xe = (xml_node<>*)xml::Path(&xdoc, 2, "lang", "item");
		while (xe)
		{
			cstr8 tmp = xml::ValueCh8(xe->first_attribute("key"));
			_Dict->Add(tmp, xml::ValueCh8(xe));
			xe = xe->next_sibling();
		}
		delete data;
	}

	cstr8 Translator::GetText(cstr8 key)
	{
		cstr8 tmp = _Dict->Find(key);
		return tmp;
	}
}