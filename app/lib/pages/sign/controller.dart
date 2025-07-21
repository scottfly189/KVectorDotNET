import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:projectzoom/generated/l10n.dart';
import 'package:projectzoom/services/user_config.dart';
import 'package:projectzoom/services/storeage.dart';
import 'package:projectzoom/utils/http_utils.dart';
import 'package:flutter_easyloading/flutter_easyloading.dart';

class SignController extends GetxController {
  final TextEditingController accountInput = TextEditingController();
  final TextEditingController passwordInput = TextEditingController();
  final isPasswordHidden = true.obs;

  @override
  void onInit() {
    super.onInit();
    accountInput.text = StorageService.to.getString('account');
  }

  @override
  void onClose() {
    accountInput.dispose();
    passwordInput.dispose();
    super.onClose();
  }

  void onShowPassword() {
    isPasswordHidden.value = !isPasswordHidden.value;
    update();
  }

  Future<bool> onSignIn(
      GlobalKey<FormState> formKey, BuildContext context) async {
    if (!formKey.currentState!.validate()) return false;
    await StorageService.to.setString('account', accountInput.text);
    try {
      EasyLoading.show(status: 'loading...');
      await HttpUtils.request<Map<String, dynamic>>(
        path: '/api/sysAuth/loginApp',
        method: 'POST',
        data: {
          'account': accountInput.text,
          'password': passwordInput.text,
        },
      );
      UserConfig.to.isLogin = true;
      return true;
    } catch (e) {
      if (context.mounted) {
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(
            content: Text(S.of(context).login_fail),
            backgroundColor: Colors.red,
            duration: Duration(seconds: 3),
            action: SnackBarAction(
              label: S.of(context).confirm,
              textColor: Colors.white,
              onPressed: () {},
            ),
          ),
        );
      }
      return false;
    } finally {
      EasyLoading.dismiss();
    }
  }
}
