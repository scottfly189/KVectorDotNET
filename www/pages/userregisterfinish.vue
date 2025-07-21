<template>
  <div class="min-h-screen bg-gray-100 flex items-center justify-center">
    <div class="w-full max-w-md p-8 bg-white rounded-lg shadow-md">
      <h1 class="text-2xl font-bold mb-6 text-center">{{ $t('register.registerWillComplete') }} - {{
        $t('register.registerWillCompleteSubtitle') }}</h1>
      <div class="space-y-4">
        <div class="space-y-4">
          <label for="verificationCode" class="block text-sm font-medium text-gray-700">{{
            $t('register.verificationCode') }}:</label>
          <div class="flex space-x-2">
            <input type="text" maxlength="1" v-model="verificationCode[0]" @input="onInput(0)"
              @keydown="onKeyDown(0, $event)" ref="inputs0"
              class="w-12 h-12 text-center border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm">
            <input type="text" maxlength="1" v-model="verificationCode[1]" @input="onInput(1)"
              @keydown="onKeyDown(1, $event)" ref="inputs1"
              class="w-12 h-12 text-center border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm">
            <input type="text" maxlength="1" v-model="verificationCode[2]" @input="onInput(2)"
              @keydown="onKeyDown(2, $event)" ref="inputs2"
              class="w-12 h-12 text-center border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm">
            <input type="text" maxlength="1" v-model="verificationCode[3]" @input="onInput(3)"
              @keydown="onKeyDown(3, $event)" ref="inputs3"
              class="w-12 h-12 text-center border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm">
            <input type="text" maxlength="1" v-model="verificationCode[4]" @input="onInput(4)"
              @keydown="onKeyDown(4, $event)" ref="inputs4"
              class="w-12 h-12 text-center border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm">
            <input type="text" maxlength="1" v-model="verificationCode[5]" @input="onInput(5)"
              @keydown="onKeyDown(5, $event)" ref="inputs5"
              class="w-12 h-12 text-center border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm">
          </div>
        </div>
        <div class="space-y-4">
          <label for="companyName" class="block text-sm font-medium text-gray-700">{{
            $t('userRegister.successTitle') }}</label>
          <input type="text" disabled id="companyName" v-model="companyName"
            class="mt-1 text-center text-blue-500 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
           >
        </div>
        <div class="space-y-4">
          <button @click="register"
            class="w-full mt-5 px-4 py-2 bg-blue-500 text-white rounded-md hover:bg-blue-600 focus:outline-none focus:ring-2 focus:ring-blue-500">{{
              $t('register.register') }}</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
// @ts-nocheck
import { ref } from "vue"
import { ElMessage, ElMessageBox } from 'element-plus'
import { useI18n } from 'vue-i18n'
import { ElLoading } from 'element-plus'
//@ts-ignore
definePageMeta({
  layout: 'register-layout',
})

const verificationCode = ref(['', '', '', '', '', '']);
const emailInfo = useState('emailInfo');
console.log('emailInfo', emailInfo.value);
const companyName = ref(emailInfo.value.orgName);

// 添加输入框引用
const inputs0 = ref<HTMLInputElement | null>(null);
const inputs1 = ref<HTMLInputElement | null>(null);
const inputs2 = ref<HTMLInputElement | null>(null);
const inputs3 = ref<HTMLInputElement | null>(null);
const inputs4 = ref<HTMLInputElement | null>(null);
const inputs5 = ref<HTMLInputElement | null>(null);

// 创建一个数组来存储所有输入框引用
const inputs = [inputs0, inputs1, inputs2, inputs3, inputs4, inputs5];

// 修改类型断言以支持带参数的翻译
const { t, locale } = useI18n() as { t: (key: string, options?: any) => string, locale: any };

const onInput = (index: number) => {
  if (verificationCode.value[index] && index < 5 && inputs[index + 1].value) {
    inputs[index + 1].value.focus();
  }
}

const onKeyDown = (index: number, event: KeyboardEvent) => {
  if (event.key === 'Backspace' && !verificationCode.value[index] && index > 0 && inputs[index - 1].value) {
    inputs[index - 1].value.focus();
  }

  if (event.key === 'ArrowLeft' && index > 0 && inputs[index - 1].value) {
    inputs[index - 1].value.focus();
  }

  if (event.key === 'ArrowRight' && index < 5 && inputs[index + 1].value) {
    inputs[index + 1].value.focus();
  }
}

const register = async () => {
  if (!_validForm()) return;
  //检查验证码是否正确
  let result = await _checkVerificationCode();
  if (!result.result) return;
  //根据邮箱、密码、公司名称创建用户
  const loadingInstance = ElLoading.service({
    lock: true,
    text: t('userRegister.joinTeam'),
    background: 'rgba(0, 0, 0, 0.7)',
  });
  const { data, error } = await useApi(`/api/sysUser/registerUser`, locale.value, {
    headers: {
      Authorization: `Bearer ${result.accessToken}`,
      'X-Authorization': `Bearer ${result.refreshToken}`,
    },
    method: 'POST',
    body: {
      email: emailInfo.value.email,
      password: emailInfo.value.password,
      tenantId: emailInfo.value.tenantId,
      teamId: emailInfo.value.teamId,
      createUserId: emailInfo.value.userId,
      role: emailInfo.value.role,
      fullName: emailInfo.value.fullName,
      phone: emailInfo.value.phone,
    }
  });
  loadingInstance.close();
  if (error.value) {
    ElMessage.error(t('userRegister.joinTeamFailed'));
    return;
  }
  if (data.value.code !== 200) {
    ElMessage.error(data.value.message);
    return;
  }
  //发送成功邮件
  await _sendSuccessEmail();
  //跳转至创建详情页
  await navigateTo('/willlogin');
}
const _sendSuccessEmail = async () => {
  //发送成功邮件
  const { data, error } = await useApi(`/api/resend/emailRegisterSuccess?email=${emailInfo.value.email}&password=${emailInfo.value.password}`, locale.value);
  console.log('发送反馈邮件情况：', data.value.result ? '成功' : '失败');
}
const _checkVerificationCode = async () => {
  //检查验证码是否正确
  const { data, error } = await useApi(`/api/resend/verifyCode?email=${emailInfo.value.email}&code=${verificationCode.value.join('')}`, locale.value);
  if (error.value) {
    ElMessage.error(t('register.verificationCodeError'));
    return {
      result: false,
      accessToken: '',
      refreshToken: ''
    };
  }
  if (data.value.code === 200 && data.value.result.isOk) {
    return {
      result: true,
      accessToken: data.value.result.accessToken,
      refreshToken: data.value.result.refreshToken
    };
  }
  ElMessage.error(t('register.verificationCodeError'));
  return {
    result: false,
    accessToken: '',
    refreshToken: ''
  };
}
const _createTenant = () => {
  //根据邮箱、密码、公司名称创建租户、用户、企业
}
const _validForm = () => {
  if (verificationCode.value.join('') === '') {
    ElMessage.error(t('register.verificationCodeRequired'));
    return false;
  }
  if (verificationCode.value.join('').length !== 6) {
    ElMessage.error(t('register.verificationCodeRequired'));
    return false;
  }
  if (companyName.value === '') {
    ElMessage.error(t('register.finishCompanyNameRequired'));
    return false;
  }
  return true;
}
const _getVerificationCode = () => {
  return verificationCode.value.join('');
}
</script>
