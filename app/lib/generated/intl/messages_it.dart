// DO NOT EDIT. This is code generated via package:intl/generate_localized.dart
// This is a library that provides messages for a it locale. All the
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
  String get localeName => 'it';

  static String m0(name) => "Ciao, ${name}";

  static String m1(name) => "Nome utente:${name}";

  final messages = _notInlinedMessages(_notInlinedMessages);
  static Map<String, Function> _notInlinedMessages(_) => <String, Function>{
    "about": MessageLookupByLibrary.simpleMessage("Informazioni"),
    "account": MessageLookupByLibrary.simpleMessage("Account"),
    "account_info": MessageLookupByLibrary.simpleMessage(
      "Informazioni account",
    ),
    "app_name": MessageLookupByLibrary.simpleMessage(
      "ProjectZoom Gestione della costruzione e dell\'impianto",
    ),
    "cancel": MessageLookupByLibrary.simpleMessage("Annulla"),
    "change_language": MessageLookupByLibrary.simpleMessage("Cambia lingua"),
    "confirm": MessageLookupByLibrary.simpleMessage("Conferma"),
    "daily_log": MessageLookupByLibrary.simpleMessage("Diario"),
    "forget_password": MessageLookupByLibrary.simpleMessage(
      "Password dimenticata?",
    ),
    "help_and_feedback": MessageLookupByLibrary.simpleMessage(
      "Aiuto e feedback",
    ),
    "hi": m0,
    "job": MessageLookupByLibrary.simpleMessage("Lavori"),
    "language": MessageLookupByLibrary.simpleMessage("Cambia lingua"),
    "login_fail": MessageLookupByLibrary.simpleMessage(
      "Errore di accesso, riprova",
    ),
    "logout": MessageLookupByLibrary.simpleMessage("Esci"),
    "logout_confirm": MessageLookupByLibrary.simpleMessage(
      "Sei sicuro di voler uscire?",
    ),
    "my": MessageLookupByLibrary.simpleMessage("Profilo"),
    "password": MessageLookupByLibrary.simpleMessage("Password"),
    "print_setting": MessageLookupByLibrary.simpleMessage(
      "Impostazioni stampa",
    ),
    "project": MessageLookupByLibrary.simpleMessage("Progetto"),
    "register": MessageLookupByLibrary.simpleMessage("Registrati"),
    "register_now": MessageLookupByLibrary.simpleMessage("Registrati ora"),
    "report": MessageLookupByLibrary.simpleMessage("Report"),
    "schedule": MessageLookupByLibrary.simpleMessage("Pianificazione"),
    "search": MessageLookupByLibrary.simpleMessage("Cerca..."),
    "select_project": MessageLookupByLibrary.simpleMessage(
      "Seleziona progetto",
    ),
    "setting": MessageLookupByLibrary.simpleMessage("Profilo"),
    "sign_in": MessageLookupByLibrary.simpleMessage("Accedi"),
    "sub_title": MessageLookupByLibrary.simpleMessage(
      "Gestione della costruzione e dell\'impianto",
    ),
    "system_setting": MessageLookupByLibrary.simpleMessage(
      "Impostazioni sistema",
    ),
    "talk_hub": MessageLookupByLibrary.simpleMessage("Talk Hub"),
    "tickets": MessageLookupByLibrary.simpleMessage("Tickets"),
    "user_info": m1,
    "version": MessageLookupByLibrary.simpleMessage("Versione"),
  };
}
