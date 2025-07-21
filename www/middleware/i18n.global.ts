import { defineNuxtRouteMiddleware, useNuxtApp } from 'nuxt/app'

export default defineNuxtRouteMiddleware((to, from) => {
  // 仅在客户端执行
  if (process.client) {
    const nuxtApp = useNuxtApp()
    const i18n = nuxtApp.$i18n as { locale: { value: string } }
    
    // 从 localStorage 获取保存的语言
    const savedLocale = localStorage.getItem('user-locale')
    
    // 如果有保存的语言且与当前不同，则应用它
    if (savedLocale && savedLocale !== i18n.locale.value) {
      i18n.locale.value = savedLocale
    }
  }
}) 