#pragma once

#include "vf_candy.h"

namespace vapula
{
	//查看语言代码参考
	VAPULA_API void SeeAlsoLangCode();

	//语言代码
	enum LangCode
	{
		af = 0 , sq, ar_AE, ar_BH, ar_DZ, ar_EG, ar_IQ, ar_JO, ar_KW, ar_LB, ar_LY, ar_MA, ar_OM, ar_QA, ar_SA,
		ar_SY, ar_TN, ar_YE, hy, az_AZ_l, az_AZ_c, eu, be, bg, ca, zh_CN, zh_HK, zh_MO, zh_SG, zh_TW, hr, cs, da,
		nl_NL, nl_BE, en_AU, en_BZ, en_CA, en_CB, en_IN, en_IE, en_JM, en_MY, en_NZ, en_PH, en_SG, en_ZA, en_TT,
		en_GB, en_US, en_ZW, et, fa, fi, fo, fr_FR, fr_BE, fr_CA, fr_LU, fr_CH, gd_IE, gd, de_DE, de_AT, de_LI,
		de_LU, de_CH, el, he, hi, hu, is, id, it_IT, it_CH, ja, ko, lv, lt, mk, ms_MY, ms_BN, mt, mr, nb_NO, nn_NO,
		pl, pt_PT, pt_BR, rm,	ro, ro_MO, ru, ru_MO, sa, sr_SP_c, sr_SP_l, tn, sl, sk, sb, es_ES, es_t, es_AR,
		es_BO, es_CL, es_CO, es_CR, es_DO, es_EC, es_GT, es_HN, es_MX, es_NI, es_PA, es_PE, es_PR, es_PY, es_SV,
		es_UY, es_VE, st, sw, sv_SE, sv_FI, ta, tt,	th, tr, ts, uk, ur, uz_UZ_c, uz_UZ_l, vi, xh, yi, zu
	};

	//获取区域代码
	int GetZoneCode(int lc);

	//获取语言代码名称
	cstr8 GetLangName(int lc);

	class VAPULA_API Translator
	{
	public:
		Translator();
		~Translator();
	private:
		cstr8 _Dir;
		Dict* _Dict;
	public:
		//设置字典路径
		//需要形如"X:\\...\\"
		void SetDictDir(cstr8 dir);

		//通过key查找文本
		cstr8 GetText(cstr8 key);

		//加载语言包
		void LoadLangPack(int lc = zh_CN);
	};
}