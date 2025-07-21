import '@vue/runtime-core'

declare module '@vue/runtime-core' {
  interface ComponentCustomProperties {
    $t: (key: string) => string
    $i18n: {
      locale: string
      locales: { code: string, name: string }[]
    }
  }
} 