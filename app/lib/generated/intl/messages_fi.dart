// DO NOT EDIT. This is code generated via package:intl/generate_localized.dart
// This is a library that provides messages for a fi locale. All the
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
  String get localeName => 'fi';

  static String m0(name) => "Hei, ${name}";

  static String m1(name) => "Käyttäjätunnus:${name}";

  final messages = _notInlinedMessages(_notInlinedMessages);
  static Map<String, Function> _notInlinedMessages(_) => <String, Function>{
    "about": MessageLookupByLibrary.simpleMessage("Tietoa"),
    "account": MessageLookupByLibrary.simpleMessage("Tili"),
    "account_info": MessageLookupByLibrary.simpleMessage("Tilin tiedot"),
    "app_name": MessageLookupByLibrary.simpleMessage(
      "ProjectZoom Rakennusten ja laitosten hallinta",
    ),
    "cancel": MessageLookupByLibrary.simpleMessage("Peruuta"),
    "change_language": MessageLookupByLibrary.simpleMessage("Kielen vaihto"),
    "confirm": MessageLookupByLibrary.simpleMessage("Vahvista"),
    "daily_log": MessageLookupByLibrary.simpleMessage("Päiväkirja"),
    "forget_password": MessageLookupByLibrary.simpleMessage(
      "Unohtuiko salasana?",
    ),
    "help_and_feedback": MessageLookupByLibrary.simpleMessage(
      "Apu ja palautteet",
    ),
    "hi": m0,
    "job": MessageLookupByLibrary.simpleMessage("Tehtävien"),
    "language": MessageLookupByLibrary.simpleMessage("Kielen vaihto"),
    "login_fail": MessageLookupByLibrary.simpleMessage(
      "Kirjautumisvirhe, yritä uudelleen",
    ),
    "logout": MessageLookupByLibrary.simpleMessage("Kirjaudu ulos"),
    "logout_confirm": MessageLookupByLibrary.simpleMessage(
      "Oletko varmasti haluamassa kirjautua ulos?",
    ),
    "my": MessageLookupByLibrary.simpleMessage("Profiili"),
    "password": MessageLookupByLibrary.simpleMessage("Salasana"),
    "print_setting": MessageLookupByLibrary.simpleMessage("Tulostusasetukset"),
    "project": MessageLookupByLibrary.simpleMessage("Projekti"),
    "register": MessageLookupByLibrary.simpleMessage("Rekisteröidy"),
    "register_now": MessageLookupByLibrary.simpleMessage("Rekisteröidy nyt"),
    "report": MessageLookupByLibrary.simpleMessage("Miten"),
    "schedule": MessageLookupByLibrary.simpleMessage("Aikataulu"),
    "search": MessageLookupByLibrary.simpleMessage("Hae..."),
    "select_project": MessageLookupByLibrary.simpleMessage("Valitse projekti"),
    "setting": MessageLookupByLibrary.simpleMessage("Profiili"),
    "sign_in": MessageLookupByLibrary.simpleMessage("Kirjaudu sisään"),
    "sub_title": MessageLookupByLibrary.simpleMessage(
      "Rakennusten ja laitosten hallinta",
    ),
    "system_setting": MessageLookupByLibrary.simpleMessage(
      "Järjestelmäasetukset",
    ),
    "talk_hub": MessageLookupByLibrary.simpleMessage("Talk Hub"),
    "tickets": MessageLookupByLibrary.simpleMessage("Tiketit"),
    "user_info": m1,
    "version": MessageLookupByLibrary.simpleMessage("Versio"),
  };
}
