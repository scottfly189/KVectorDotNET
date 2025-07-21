// DO NOT EDIT. This is code generated via package:intl/generate_localized.dart
// This is a library that provides messages for a pl locale. All the
// messages from the main program should be duplicated here with the same
// function name.

// Ignore issues from commonly used lints in this file.
// ignore_for_file:unnecessary_brace_in_string_interps, unnecessary_new
// ignore_for_file:prefer_single_quotes,comment_references, directives_ordering
// ignore_for_file:annotate_overrides,prefer_generic_function_type_aliases
// ignore_for_file:unused_import, file_names, avoid_escaping_inner_quotes
// ignore_for_file:unnecessary_string_interpolations, unnecessary_string_escapes

import 'package:intl/intl.dart';
import 'package:intl/message_lookup_by_library.dart';

final messages = new MessageLookup();

typedef String MessageIfAbsent(String messageStr, List<dynamic> args);

class MessageLookup extends MessageLookupByLibrary {
  String get localeName => 'pl';

  static String m0(name) => "Cześć, ${name}";

  static String m1(name) => "Nazwa użytkownika:${name}";

  final messages = _notInlinedMessages(_notInlinedMessages);
  static Map<String, Function> _notInlinedMessages(_) => <String, Function>{
    "about": MessageLookupByLibrary.simpleMessage("O"),
    "account": MessageLookupByLibrary.simpleMessage("Konto"),
    "account_info": MessageLookupByLibrary.simpleMessage("Informacje o koncie"),
    "app_name": MessageLookupByLibrary.simpleMessage(
      "ProjectZoom Zarządzanie budową i infrastrukturą",
    ),
    "cancel": MessageLookupByLibrary.simpleMessage("Anuluj"),
    "change_language": MessageLookupByLibrary.simpleMessage("Zmień język"),
    "confirm": MessageLookupByLibrary.simpleMessage("Potwierdź"),
    "daily_log": MessageLookupByLibrary.simpleMessage("Dziennik"),
    "forget_password": MessageLookupByLibrary.simpleMessage(
      "Zapomniałeś hasła?",
    ),
    "help_and_feedback": MessageLookupByLibrary.simpleMessage("Pomoc i opinie"),
    "hi": m0,
    "job": MessageLookupByLibrary.simpleMessage("Zadania"),
    "language": MessageLookupByLibrary.simpleMessage("Zmień język"),
    "login_fail": MessageLookupByLibrary.simpleMessage(
      "Logowanie nie powiodło się, spróbuj ponownie",
    ),
    "logout": MessageLookupByLibrary.simpleMessage("Wyloguj się"),
    "logout_confirm": MessageLookupByLibrary.simpleMessage(
      "Czy na pewno chcesz się wylogować?",
    ),
    "my": MessageLookupByLibrary.simpleMessage("Ustawienia"),
    "password": MessageLookupByLibrary.simpleMessage("Hasło"),
    "print_setting": MessageLookupByLibrary.simpleMessage(
      "Ustawienia drukowania",
    ),
    "project": MessageLookupByLibrary.simpleMessage("Projekt"),
    "register": MessageLookupByLibrary.simpleMessage("Zarejestruj się"),
    "register_now": MessageLookupByLibrary.simpleMessage(
      "Zarejestruj się teraz",
    ),
    "report": MessageLookupByLibrary.simpleMessage("Raport"),
    "schedule": MessageLookupByLibrary.simpleMessage("Harmonogram"),
    "search": MessageLookupByLibrary.simpleMessage("Szukaj..."),
    "select_project": MessageLookupByLibrary.simpleMessage("Wybierz projekt"),
    "setting": MessageLookupByLibrary.simpleMessage("Ustawienia"),
    "sign_in": MessageLookupByLibrary.simpleMessage("Zaloguj się"),
    "sub_title": MessageLookupByLibrary.simpleMessage(
      "Zarządzanie budową i infrastrukturą",
    ),
    "system_setting": MessageLookupByLibrary.simpleMessage(
      "Ustawienia systemu",
    ),
    "talk_hub": MessageLookupByLibrary.simpleMessage("Talk Hub"),
    "tickets": MessageLookupByLibrary.simpleMessage("Bilety"),
    "user_info": m1,
    "version": MessageLookupByLibrary.simpleMessage("Wersja"),
  };
}
