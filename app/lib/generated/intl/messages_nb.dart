// DO NOT EDIT. This is code generated via package:intl/generate_localized.dart
// This is a library that provides messages for a nb locale. All the
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
  String get localeName => 'nb';

  static String m0(name) => "Hei, ${name}";

  static String m1(name) => "Brukernavn:${name}";

  final messages = _notInlinedMessages(_notInlinedMessages);
  static Map<String, Function> _notInlinedMessages(_) => <String, Function>{
    "about": MessageLookupByLibrary.simpleMessage("Om"),
    "account": MessageLookupByLibrary.simpleMessage("Konto"),
    "account_info": MessageLookupByLibrary.simpleMessage("Kontoinformasjon"),
    "app_name": MessageLookupByLibrary.simpleMessage(
      "ProjectZoom Bygging og anleggsforvaltning",
    ),
    "cancel": MessageLookupByLibrary.simpleMessage("Avbryt"),
    "change_language": MessageLookupByLibrary.simpleMessage("Endre språk"),
    "confirm": MessageLookupByLibrary.simpleMessage("Bekreft"),
    "daily_log": MessageLookupByLibrary.simpleMessage("Dagbok"),
    "forget_password": MessageLookupByLibrary.simpleMessage("Glemt passord?"),
    "help_and_feedback": MessageLookupByLibrary.simpleMessage(
      "Hjelp og tilbakemelding",
    ),
    "hi": m0,
    "job": MessageLookupByLibrary.simpleMessage("Jobber"),
    "language": MessageLookupByLibrary.simpleMessage("Endre språk"),
    "login_fail": MessageLookupByLibrary.simpleMessage(
      "Innlogging feil, prøv igjen",
    ),
    "logout": MessageLookupByLibrary.simpleMessage("Logg ut"),
    "logout_confirm": MessageLookupByLibrary.simpleMessage(
      "Er du sikker på at du vil logge ut?",
    ),
    "my": MessageLookupByLibrary.simpleMessage("Innstillinger"),
    "password": MessageLookupByLibrary.simpleMessage("Passord"),
    "print_setting": MessageLookupByLibrary.simpleMessage(
      "Skriv ut innstillinger",
    ),
    "project": MessageLookupByLibrary.simpleMessage("Prosjekt"),
    "register": MessageLookupByLibrary.simpleMessage("Registrer"),
    "register_now": MessageLookupByLibrary.simpleMessage("Registrer nå"),
    "report": MessageLookupByLibrary.simpleMessage("Rapport"),
    "schedule": MessageLookupByLibrary.simpleMessage("Plan"),
    "search": MessageLookupByLibrary.simpleMessage("Søk..."),
    "select_project": MessageLookupByLibrary.simpleMessage("Velg prosjekt"),
    "setting": MessageLookupByLibrary.simpleMessage("Innstillinger"),
    "sign_in": MessageLookupByLibrary.simpleMessage("Logg inn"),
    "sub_title": MessageLookupByLibrary.simpleMessage(
      "Bygging og anleggsforvaltning",
    ),
    "system_setting": MessageLookupByLibrary.simpleMessage(
      "Systeminnstillinger",
    ),
    "talk_hub": MessageLookupByLibrary.simpleMessage("Talk Hub"),
    "tickets": MessageLookupByLibrary.simpleMessage("Tiketter"),
    "user_info": m1,
    "version": MessageLookupByLibrary.simpleMessage("Versjon"),
  };
}
