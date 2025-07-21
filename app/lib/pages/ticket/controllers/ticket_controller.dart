import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:projectzoom/utils/http_utils.dart';

class TicketsController extends GetxController {
  final RxInt todayInStoreCount = 0.obs;
  final RxInt yesterdayInStoreCount = 0.obs;
  final RxInt todayConsolidationCount = 0.obs;
  final RxInt yesterdayConsolidationCount = 0.obs;
  final RxInt monthInStoreCount = 0.obs;
  final RxInt yearInStoreCount = 0.obs;
  final RxInt monthConsolidationCount = 0.obs;
  final RxInt yearConsolidationCount = 0.obs;
  final RxString selectedConsolidationStore = ''.obs;

  @override
  void onInit() {
    super.onInit();
    //这里应该从数据库中获取值.
    getTransferCount();
    //演示数据.
    monthInStoreCount.value = 2000;
    yearInStoreCount.value = 3000;
    monthConsolidationCount.value = 2000;
    yearConsolidationCount.value = 4000;
  }

  Future<void> getTransferCount() async {}
}
