class LanguageList {
  static List<LanguageItem> languageList = [
    LanguageItem(
      name: "Deutsch",
      code: "de",
      isoLanguageCode: "de",
      countryCode: "",
    ),
    LanguageItem(
      name: "English",
      code: "en",
      isoLanguageCode: "en",
      countryCode: "",
    ),
    LanguageItem(
      name: "Español",
      code: "es",
      isoLanguageCode: "es",
      countryCode: "",
    ),
    LanguageItem(
      name: "Suomeksi",
      code: "fi",
      isoLanguageCode: "fi",
      countryCode: "",
    ),
    LanguageItem(
      name: "Français",
      code: "fr",
      isoLanguageCode: "fr",
      countryCode: "",
    ),
    LanguageItem(
      name: "Italiano",
      code: "it",
      isoLanguageCode: "it",
      countryCode: "",
    ),
    LanguageItem(
      name: "日本語",
      code: "ja",
      isoLanguageCode: "ja",
      countryCode: "",
    ),
    LanguageItem(
      name: "한국어",
      code: "ko",
      isoLanguageCode: "ko",
      countryCode: "",
    ),
    LanguageItem(
      name: "Norwegian",
      code: "nb",
      isoLanguageCode: "no",
      countryCode: "",
    ),
    LanguageItem(
      name: "Polski",
      code: "pl",
      isoLanguageCode: "pl",
      countryCode: "",
    ),
    LanguageItem(
      name: "Português",
      code: "pt",
      isoLanguageCode: "pt",
      countryCode: "",
    ),
    LanguageItem(
      name: "Русский",
      code: "ru",
      isoLanguageCode: "ru",
      countryCode: "",
    ),
    LanguageItem(
      name: "简体中文",
      code: "zh",
      isoLanguageCode: "zh-CN",
      countryCode: "CN",
    ),
    LanguageItem(
      name: "繁體中文(台灣)",
      code: "zh",
      isoLanguageCode: "zh-TW",
      countryCode: "TW",
    ),
    LanguageItem(
      name: "繁體中文(香港)",
      code: "zh",
      isoLanguageCode: "zh-CN",
      countryCode: "HK",
    ),
    LanguageItem(
      name: "Indonesia",
      code: "id",
      isoLanguageCode: "id",
      countryCode: "ID",
    ),
    LanguageItem(
      name: "Việt Nam",
      code: "vi",
      isoLanguageCode: "vi",
      countryCode: "VN",
    ),
    LanguageItem(
      name: "Malaysia",
      code: "ms",
      isoLanguageCode: "ms",
      countryCode: "MY",
    ),
    LanguageItem(
      name: "ประเทศไทย",
      code: "th",
      isoLanguageCode: "th",
      countryCode: "TH",
    ),
  ];
}

class LanguageItem {
  final String name;
  final String code;
  final String isoLanguageCode;
  final String countryCode;

  LanguageItem(
      {required this.name,
      required this.code,
      required this.isoLanguageCode,
      required this.countryCode});

  Map<String, dynamic> toJson() => {
        "name": name,
        "code": code,
        "isoLanguageCode": isoLanguageCode,
        "countryCode": countryCode,
      };
}
