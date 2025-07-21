import 'package:flutter/material.dart';

abstract class Validator<T> {
  final String error;

  Validator(this.error);

  bool isValid(T value);

  String? call(T value) => isValid(value) ? null : error;

  bool hasMatch(
    String pattern,
    String value, {
    bool caseSensitive = true,
  }) =>
      RegExp(
        pattern,
        caseSensitive: caseSensitive,
      ).hasMatch(value);
}

class RequiredValidator extends Validator<String?> {
  RequiredValidator() : super('必填');

  @override
  bool isValid(String? value) => (value ?? '').isNotEmpty;
}

class EmailValidator extends Validator<String?> {
  final Pattern _pattern =
      r'^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$';

  EmailValidator() : super('邮箱格式错误');

  @override
  bool isValid(String? value) {
    if ((value ?? '').isEmpty) return true;
    return hasMatch(_pattern.toString(), value!, caseSensitive: false);
  }
}

class PasswordValidator extends Validator<String?> {
  final Pattern _pattern = r'^[0-9A-Za-z]{6,}$';

  PasswordValidator() : super('密码格式错误');

  @override
  bool isValid(String? value) {
    if ((value ?? '').isEmpty) return false;
    return hasMatch(_pattern.toString(), value!, caseSensitive: false);
  }
}

class MobileValidator extends Validator<String?> {
  MobileValidator() : super('手机号格式错误');

  @override
  bool isValid(String? value) => hasMatch(r'^1[3-9][0-9]{9}$', value ?? '');
}

class EqualValidator extends Validator<String?> {
  final TextEditingController input;

  EqualValidator(this.input) : super('密码不一致');

  @override
  bool isValid(String? value) {
    if ((value ?? '').isEmpty) return true;
    return input.value.text == value;
  }
}

class AmountValidator extends Validator<String?> {
  final Pattern _pattern =
      r'(^[1-9]([0-9]+)?(\.[0-9]{1,2})?$)|(^(0){1}$)|(^[0-9]\.[0-9]([0-9])?$)';

  AmountValidator() : super('金额格式错误');

  @override
  bool isValid(String? value) {
    if ((value ?? '').isEmpty) return true;
    return hasMatch(_pattern.toString(), value!, caseSensitive: false);
  }
}
