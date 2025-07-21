import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:projectzoom/utils/http_utils.dart';

class JobPageController extends GetxController {
  final RxInt todayRevCount = 0.obs;
  final RxInt todayDispatchCount = 0.obs;
  final RxInt todayFinishCount = 0.obs;
  final RxInt yesterdayRevCount = 0.obs;
  final RxInt yesterdayDispatchCount = 0.obs;
  final RxInt yesterdayFinishCount = 0.obs;

  @override
  void onInit() {
    super.onInit();
    //这里应该从数据库中获取值.
    getRevCount();
  }

  Future<void> getRevCount() async {
    // try {
    //   final response = await HttpUtils.request(
    //     path: '/api/dataAggregation/getRevDispatchData',
    //     method: 'POST',
    //   );
    //   if (response != null) {
    //     todayRevCount.value = response['result']["todayReceived"];
    //     yesterdayRevCount.value = response['result']["yesterdayReceived"];
    //     todayDispatchCount.value = response['result']["todayDispatch"];
    //     yesterdayDispatchCount.value = response['result']["yesterdayDispatch"];
    //     todayFinishCount.value = response['result']["todaySign"];
    //     yesterdayFinishCount.value = response['result']["yesterdaySign"];
    //     update();
    //   }
    // } catch (e) {
    //   Get.snackbar(
    //     '错误',
    //     '获取今日数据失败: $e',
    //     backgroundColor: Colors.red,
    //     colorText: Colors.white,
    //   );
    // }
  }
}
