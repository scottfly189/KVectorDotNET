import { defineNuxtConfig } from 'nuxt/config'

export default defineNuxtConfig({
  compatibilityDate: '2024-11-01',
  devtools: { enabled: true },
  modules: [
    '@nuxtjs/tailwindcss',
    '@nuxt/image',
    'nuxt-icon',
    '@nuxtjs/i18n',
    '@element-plus/nuxt'
  ],
  // @ts-ignore - Element Plus 模块类型问题
  elementPlus: {},
  // @ts-ignore - i18n 模块类型问题
  i18n: {
    vueI18n: '~/i18n.config.ts',
    locales: [
      {
        code: 'en',
        name: 'English'
      },
      {
        code: 'de',
        name: 'Deutsch'
      },
      {
        code: 'es',
        name: 'Español'
      },
      {
        code: 'fi',
        name: 'Suomi'
      },
      {
        code: 'fr',
        name: 'Français'
      },
      {
        code: 'it',
        name: 'Italiano'
      },
      {
        code: 'ja',
        name: '日本語'
      },
      {
        code: 'ko',
        name: '한국어'
      },
      {
        code: 'no',
        name: 'Norsk'
      },
      {
        code: 'pl',
        name: 'Polski'
      },
      {
        code: 'pt',
        name: 'Português'
      },
      {
        code: 'ru',
        name: 'Русский'
      },
      {
        code: 'th',
        name: 'ไทย'
      },
      {
        code: 'id',
        name: 'Indonesia'
      },
      {
        code: 'ms',
        name: 'Malaysia'
      },
      {
        code: 'zh-CN',
        name: '简体中文'
      },
      {
        code: 'zh-HK',
        name: '繁體中文(香港)'
      },
      {
        code: 'zh-TW',
        name: '繁體中文(台灣)'
      }
    ],
    defaultLocale: 'en',
    strategy: 'no_prefix',
    detectBrowserLanguage: {
      useCookie: true,
      cookieKey: 'i18n_redirected',
      redirectOn: 'root',
      alwaysRedirect: true
    }
  },
  css: [
    '~/assets/css/main.css',
    'element-plus/dist/index.css',
  ],
  app: {
    head: {
      title: 'Construction, Facility Management & Real Estate Platform - Simplify Your Workday',
      meta: [
        { charset: 'utf-8' },
        { name: 'viewport', content: 'width=device-width, initial-scale=1' },
        { name: 'description', content: 'Construction, Facility Management & Real Estate Platform - Simplify Your Workday' }
      ],
      link: [
        { rel: 'icon', type: 'image/x-icon', href: '/favicon.ico' }
      ]
    }
  },
  nitro: {
    esbuild: {
      options: {
        logOverride: {
          'PACKAGE_PATH_RESOLUTION_WARNING': 'silent'
        }
      }
    }
  },
  runtimeConfig: {
    public: {
      apiUrl: process.env.NUXT_PUBLIC_API_BASE
    }
  }
})
