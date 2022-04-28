using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LocalizationTutorial
{
	public class ChangeLanguage : MonoBehaviour
	{
		
		public void SetEnglish()
		{
			CGT.LocalizationManager.instance.localizationConfiguration.currentLanguage = CGT.LocalizationConfiguration.Language.English;
		}

		public void SetSpanish()
		{
			CGT.LocalizationManager.instance.localizationConfiguration.currentLanguage = CGT.LocalizationConfiguration.Language.Spanish;
		}

		public void SetJapanese()
		{
			CGT.LocalizationManager.instance.localizationConfiguration.currentLanguage = CGT.LocalizationConfiguration.Language.Japanese;
		}
	}
}