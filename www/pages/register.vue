<template>
  <div class="min-h-screen bg-gray-100 flex items-center justify-center">
    <div class="w-full max-w-md p-8 bg-white rounded-lg shadow-md">
      <h1 class="text-2xl font-bold mb-6 text-center">{{ $t('register.title') }} - {{ $t('register.subtitle') }}</h1>
      <div class="space-y-4">
        <div class="space-y-2">
          <label for="email" class="block text-sm font-medium text-gray-700">{{ $t('register.email') }}:</label>
          <input type="email" id="email" v-model="email"
            class="w-full px-3 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
            :placeholder="$t('register.requireEmail')" />
        </div>
        <div class="space-y-2">
          <label for="password" class="block text-sm font-medium text-gray-700">{{ $t('register.password') }}:</label>
          <input type="password" id="password" v-model="password"
            class="w-full px-3 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
            :placeholder="$t('register.requirePassword')" />
        </div>
        <div class="space-y-2">
          <label for="confirmPassword" class="block text-sm font-medium text-gray-700">{{ $t('register.confirmPassword')
          }}:</label>
          <input type="password" id="confirmPassword" v-model="confirmPassword"
            class="w-full px-3 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
            :placeholder="$t('register.requireConfirmPassword')" />
        </div>
        <div class="space-y-2">
          <label for="phone" class="block text-sm font-medium text-gray-700">{{ $t('register.mobile') }}:</label>
          <input id="phone" v-model="phone"
            class="w-full px-3 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
            :placeholder="$t('register.registerErrorMobileRequired')" />
        </div>
        <div class="space-y-2">
          <label for="agreement" class="block text-sm font-medium text-gray-700">{{
            $t('register.requireAcceptMarketing') }}</label>
          <div class="flex items-center">
            <input v-model="agreement" type="checkbox" id="agreement" class="form-checkbox h-5 w-5 text-blue-500" />
            <label for="agreement" class="ml-2 text-sm text-gray-700">{{ $t('register.confirmMarcketing') }}</label>
          </div>
        </div>
        <div class="space-y-4">
          <button @click="register" 
            :disabled="!agreement"
            :class="[
              'w-full mt-5 px-4 py-2 text-white rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500',
              agreement ? 'bg-blue-500 hover:bg-blue-600' : 'bg-gray-400 cursor-not-allowed opacity-70'
            ]">{{
              $t('common.next') }}</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
//@ts-ignore
import { ref } from 'vue'
import { ElMessage, ElMessageBox } from 'element-plus'
import { navigateTo } from 'nuxt/app';
import { useI18n } from 'vue-i18n';
import { ElLoading } from 'element-plus'

//@ts-ignore
definePageMeta({
  layout: 'register-layout',
})
const email = ref('');
const password = ref('');
const confirmPassword = ref('');
const agreement = ref(false);
const phone = ref('');
const emailInfo = useState('emailInfo', () => {
  return {
    email: '',
    password: '',
    phone: ''
  }
})
// 修改类型断言以支持带参数的翻译和locale属性
const { t, locale } = useI18n() as { t: (key: string, options?: any) => string; locale: Ref<string> };

const register = () => {
  if (!_valid()) return;
  let message = t('register.querySendEmail', { email: email.value });

  // 使用 ElMessageBox 显示确认对话框
  ElMessageBox.confirm(message, t('common.confirm'), {
    confirmButtonText: t('common.ok'),
    cancelButtonText: t('common.cancel'),
    type: 'info'
  }).then(async () => {
    // 发送注册请求的代码
    const loadingInstance = ElLoading.service({
      lock: true,
      text: t('register.sendingEmail'),
      background: 'rgba(0, 0, 0, 0.7)',
    });    
    const { data, error } = await useApi(`/api/resend/emailRegister?email=${email.value}`, locale.value);
    loadingInstance.close();
    if (error.value) {
      ElMessage.error(t('register.emailSentError'));
    } else {
      emailInfo.value.email = email.value;
      emailInfo.value.password = password.value;
      emailInfo.value.phone = phone.value;
      console.log('emailInfo',emailInfo.value);
      if (data.value.code === 200 && data.value.result) {
        await navigateTo('/register-finish');
      } else {
        ElMessage.error(t('register.emailSentError'));
      }
    }
  }).catch((error) => {
    console.error(error);
    // 用户取消
  });
}
// 验证表单
const _valid = (): boolean => {
  if (!email.value) {
    ElMessage.error(t('register.registerErrorEmailRequired'))
    return false;
  }
  if (!password.value) {
    ElMessage.error(t('register.registerErrorPasswordRequired'))
    return false;
  }
  if (password.value !== confirmPassword.value) {
    ElMessage.error(t('register.registerErrorConfirmPassword'))
    return false;
  }
  if (!phone.value) {
    ElMessage.error(t('register.registerErrorMobileRequired'))
    return false;
  }
  if (!_validEmail()) {
    ElMessage.error(t('register.registerErrorEmailInvalid'))
    return false;
  }
  if (!agreement.value) {
    ElMessage.error(t('register.registerErrorConfirmMarcketing'))
    return false;
  }
  return true;
}
// 验证邮箱
const _validEmail = (): boolean => {
  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  return emailRegex.test(email.value);
}

</script>
