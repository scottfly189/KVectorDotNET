import { defineNuxtPlugin } from 'nuxt/app'

export default defineNuxtPlugin((nuxtApp) => {
  // 仅在客户端执行
  if (process.client) {
    // 从 localStorage 获取保存的语言
    const savedLocale = localStorage.getItem('user-locale')
    
    // 使用类型断言
    const i18n = nuxtApp.$i18n as { locale: { value: string } }
    
    // 如果有保存的语言且与当前不同，则应用它
    if (savedLocale && savedLocale !== i18n.locale.value) {
      i18n.locale.value = savedLocale
    }
  }
}) 