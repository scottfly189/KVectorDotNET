<template>
  <div class="min-h-0 py-10 bg-gray-100 flex items-center justify-center">
    <div class="w-full max-w-md p-8 bg-white rounded-lg shadow-md">
      <div class="flex justify-center items-center">
        <img src="/images/work.png" alt="success" class="w-65 h-48">
      </div>
      <h1 class="text-2xl font-bold mb-6 text-center">{{ $t('register.registerSuccess') }}</h1>
      <div class="space-y-4 text-center">
        <p>{{ $t('register.registerSuccessSubtitle') }}!</p>
      </div>

      <div class="space-y-4">
        <button @click="login"
          class="w-full mt-5 px-4 py-2 bg-blue-500 text-white rounded-md hover:bg-blue-600 focus:outline-none focus:ring-2 focus:ring-blue-500">{{
            $t('common.login') }}</button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
// @ts-ignore
import { ref } from "vue"
// @ts-ignore
import { useI18n } from 'vue-i18n';

//@ts-ignore
definePageMeta({
  layout: 'register-layout',
})

// 修改类型断言以支持带参数的翻译
const { t, locale } = useI18n() as { t: (key: string, options?: any) => string, locale: any };

const login = async () => {
  window.open('https://admin.projectzoom.com.au', '_blank');
}

</script>
