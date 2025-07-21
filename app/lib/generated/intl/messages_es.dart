// DO NOT EDIT. This is code generated via package:intl/generate_localized.dart
// This is a library that provides messages for a es locale. All the
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
  String get localeName => 'es';

  static String m0(name) => "Hola, ${name}";

  static String m1(name) => "Nombre de usuario:${name}";

  final messages = _notInlinedMessages(_notInlinedMessages);
  static Map<String, Function> _notInlinedMessages(_) => <String, Function>{
    "about": MessageLookupByLibrary.simpleMessage("Acerca de"),
    "account": MessageLookupByLibrary.simpleMessage("Cuenta"),
    "account_info": MessageLookupByLibrary.simpleMessage(
      "Información de la cuenta",
    ),
    "app_name": MessageLookupByLibrary.simpleMessage(
      "ProjectZoom Gestión de construcción y mantenimiento",
    ),
    "cancel": MessageLookupByLibrary.simpleMessage("Cancelar"),
    "change_language": MessageLookupByLibrary.simpleMessage("Cambiar idioma"),
    "confirm": MessageLookupByLibrary.simpleMessage("Confirmar"),
    "daily_log": MessageLookupByLibrary.simpleMessage("Daily Log"),
    "forget_password": MessageLookupByLibrary.simpleMessage(
      "Olvidé mi contraseña?",
    ),
    "help_and_feedback": MessageLookupByLibrary.simpleMessage(
      "Ayuda y feedback",
    ),
    "hi": m0,
    "job": MessageLookupByLibrary.simpleMessage("Aufgabe"),
    "language": MessageLookupByLibrary.simpleMessage("Cambiar idioma"),
    "login_fail": MessageLookupByLibrary.simpleMessage(
      "Error de inicio de sesión, por favor intente nuevamente",
    ),
    "logout": MessageLookupByLibrary.simpleMessage("Cerrar sesión"),
    "logout_confirm": MessageLookupByLibrary.simpleMessage(
      "¿Estás seguro de querer cerrar sesión?",
    ),
    "my": MessageLookupByLibrary.simpleMessage("Perfil"),
    "password": MessageLookupByLibrary.simpleMessage("Contraseña"),
    "print_setting": MessageLookupByLibrary.simpleMessage(
      "Configuración de impresión",
    ),
    "project": MessageLookupByLibrary.simpleMessage("Proyecto"),
    "register": MessageLookupByLibrary.simpleMessage("Registrar"),
    "register_now": MessageLookupByLibrary.simpleMessage("Registrarse ahora"),
    "report": MessageLookupByLibrary.simpleMessage("Informe"),
    "schedule": MessageLookupByLibrary.simpleMessage("Programación"),
    "search": MessageLookupByLibrary.simpleMessage("Buscar..."),
    "select_project": MessageLookupByLibrary.simpleMessage(
      "Seleccionar proyecto",
    ),
    "setting": MessageLookupByLibrary.simpleMessage("Perfil"),
    "sign_in": MessageLookupByLibrary.simpleMessage("Iniciar sesión"),
    "sub_title": MessageLookupByLibrary.simpleMessage(
      "Gestión de construcción y mantenimiento",
    ),
    "system_setting": MessageLookupByLibrary.simpleMessage(
      "Configuración del sistema",
    ),
    "talk_hub": MessageLookupByLibrary.simpleMessage("Talk Hub"),
    "tickets": MessageLookupByLibrary.simpleMessage("Ticket"),
    "user_info": m1,
    "version": MessageLookupByLibrary.simpleMessage("Versión"),
  };
}
