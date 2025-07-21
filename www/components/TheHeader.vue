<template>
  <header class="bg-white shadow-sm sticky top-0 z-50" style="position: relative;">
    <div class="container mx-auto px-4">
      <div class="flex justify-between items-center py-6">
        <!-- Logo -->
        <NuxtLink to="/" class="flex items-center space-x-2">
          <ConstructionLogo width="36" height="36" />
          <span class="text-2xl font-bold">{{ $t('siteName') }}</span>
        </NuxtLink>

        <!-- 主导航 -->
        <nav class="hidden lg:flex space-x-12">
          <div v-for="(item, index) in mainNavItems" :key="index" class="relative nav-group">
            <template v-if="item.sections">
              <button class="flex items-center space-x-2 py-2 hover:text-blue-600 text-lg">
                <span>{{ $t(`header.${item.key}`) }}</span>
                <Icon name="heroicons:chevron-down" class="w-4 h-4" />
              </button>
              <div class="dropdown-menu">
                <div class="bg-white shadow-lg rounded-md p-4">
                  <div v-for="(section, sectionIndex) in item.sections" :key="sectionIndex" class="mb-4">
                    <h3 class="font-bold text-gray-500 mb-2">{{
                      $t(`header.${item.key}Menu.sections.${section.key}.title`) }}</h3>
                    <div class="space-y-2">
                      <NuxtLink v-for="link in section.links" :key="link.key" :to="link.url"
                        class="block hover:text-blue-600">
                        {{ $t(`header.${item.key}Menu.sections.${section.key}.links.${link.key}`) }}
                      </NuxtLink>
                    </div>
                  </div>
                </div>
              </div>
            </template>
            <NuxtLink v-else :to="item.url ?? '/'" class="py-2 hover:text-blue-600 text-lg">
              {{ $t(`header.${item.key}`) }}
            </NuxtLink>
          </div>
        </nav>

        <!-- 右侧操作区 -->
        <div class="flex items-center space-x-4">
          <!-- 语言选择器 - 图标式 -->
          <div class="relative" ref="languageMenuRef" style="position: absolute; right: 20px;top:5px;">
            <button @click="toggleLanguageMenu" class="flex items-center text-gray-700 hover:text-blue-600">
              <Icon name="heroicons:globe-alt" class="w-5 h-5" />
              <span class="ml-1 text-sm">{{ currentLocaleName }}</span>
            </button>
            <!-- 语言下拉菜单 -->
            <div v-if="showLanguageMenu" class="absolute right-0 mt-2 w-48 bg-white shadow-lg rounded-md py-2 z-50">
              <button v-for="loc in locales" :key="loc.code" @click="changeLanguage(loc.code)"
                class="w-full text-left px-4 py-2 hover:bg-gray-100"
                :class="{ 'text-blue-600 font-medium': locale === loc.code }">
                {{ loc.name }}
              </button>
            </div>
          </div>

          <!-- 登录按钮 -->
          <a href="https://admin.projectzoom.com.au/" target="_blank" class="hidden md:block border border-yellow-500 text-yellow-500 px-4 py-2 rounded-md hover:bg-yellow-500 hover:text-white">
            {{ $t('common.login') }}
          </a>

          <!-- 注册按钮 -->
          <NuxtLink to="/register"
            class="hidden md:block bg-blue-600 text-white px-4 py-2 rounded-md hover:bg-blue-700">
            {{ $t('common.signUp') }}
          </NuxtLink>
        </div>

        <!-- 移动端菜单按钮 -->
        <button @click="toggleMobileMenu" class="lg:hidden text-gray-600 hover:text-blue-600">
          <Icon name="heroicons:bars-3" class="w-6 h-6" />
        </button>
      </div>
    </div>

    <!-- 移动端菜单 -->
    <div v-if="showMobileMenu" class="lg:hidden bg-white shadow-lg absolute top-full left-0 right-0 z-50">
      <div class="container mx-auto px-4 py-4">
        <!-- 移动端导航项 -->
        <div v-for="(item, index) in mainNavItems" :key="index" class="border-b border-gray-100 py-2">
          <!-- 有子菜单的项目 -->
          <div v-if="item.sections">
            <div @click="toggleMobileSubmenu(index)" class="flex justify-between items-center py-2">
              <span class="text-lg">{{ $t(`header.${item.key}`) }}</span>
              <Icon :name="mobileExpandedItems[index] ? 'heroicons:chevron-up' : 'heroicons:chevron-down'" class="w-4 h-4" />
            </div>
            
            <!-- 子菜单 -->
            <div v-if="mobileExpandedItems[index]" class="pl-4 mt-2 space-y-2">
              <div v-for="(section, sectionIndex) in item.sections" :key="sectionIndex" class="mb-4">
                <h3 class="font-bold text-gray-500 mb-2">{{ $t(`header.${item.key}Menu.sections.${section.key}.title`) }}</h3>
                <div class="space-y-2">
                  <NuxtLink 
                    v-for="link in section.links" 
                    :key="link.key" 
                    :to="link.url" 
                    class="block hover:text-blue-600 py-1"
                    @click="showMobileMenu = false"
                  >
                    {{ $t(`header.${item.key}Menu.sections.${section.key}.links.${link.key}`) }}
                  </NuxtLink>
                </div>
              </div>
            </div>
          </div>
          
          <!-- 无子菜单的项目 -->
          <NuxtLink 
            v-else 
            :to="item.url ?? '/'" 
            class="block py-2 text-lg"
            @click="showMobileMenu = false"
          >
            {{ $t(`header.${item.key}`) }}
          </NuxtLink>
        </div>
        
        <!-- 移动端登录/注册按钮 -->
        <div class="flex flex-col space-y-2 mt-4">
          <a 
            href="https://admin.projectzoom.com.au/" 
            target="_blank"
            class="border border-yellow-500 text-yellow-500 px-4 py-2 rounded-md hover:bg-yellow-500 hover:text-white text-center"
            @click="showMobileMenu = false"
          >
            {{ $t('common.login') }}
        </a>
          <NuxtLink 
            to="/register" 
            class="bg-blue-600 text-white px-4 py-2 rounded-md hover:bg-blue-700 text-center"
            @click="showMobileMenu = false"
          >
            {{ $t('common.signUp') }}
          </NuxtLink>
        </div>
      </div>
    </div>
  </header>
</template>

<script setup lang="ts">
import { ref, computed, onMounted, onUnmounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { useNuxtApp } from 'nuxt/app'
import { useRouter, useRoute } from 'vue-router'

interface I18nInstance {
  locales: {
    value: Array<{
      code: string;
      name: string;
    }>;
  };
}

const { t, locale } = useI18n()
const nuxtApp = useNuxtApp()
const router = useRouter()
const route = useRoute()
const locales = computed(() => (nuxtApp.$i18n as I18nInstance).locales.value)
const showLanguageMenu = ref(false)
const languageMenuRef = ref<HTMLElement | null>(null)

const toggleLanguageMenu = () => {
  showLanguageMenu.value = !showLanguageMenu.value
}

const changeLanguage = (code: string) => {
  if (code === locale.value) {
    showLanguageMenu.value = false
    return // 如果语言相同，不做任何操作
  }

  // 设置 localStorage
  localStorage.setItem('user-locale', code)

  // 设置 cookie (可选，与 detectBrowserLanguage 配合使用)
  const date = new Date()
  date.setTime(date.getTime() + 365 * 24 * 60 * 60 * 1000)
  document.cookie = `i18n_redirected=${code}; expires=${date.toUTCString()}; path=/`

  // 更新语言
  locale.value = code

  // 不再需要导航到新路径，因为我们不使用 URL 前缀

  showLanguageMenu.value = false
}

// 获取当前语言名称
const currentLocaleName = computed(() => {
  const currentLocale = locales.value.find(l => l.code === locale.value)
  return currentLocale ? currentLocale.name : ''
})

// 点击外部关闭菜单
const handleClickOutside = (event: MouseEvent) => {
  // 语言菜单关闭逻辑
  if (languageMenuRef.value && !languageMenuRef.value.contains(event.target as Node)) {
    showLanguageMenu.value = false
  }
  
  // 移动端菜单关闭逻辑
  const target = event.target as HTMLElement
  if (showMobileMenu.value && !target.closest('.lg\\:hidden') && !target.closest('button')) {
    showMobileMenu.value = false
  }
}

// 只保留一个事件监听器
onMounted(() => {
  document.addEventListener('click', handleClickOutside)
})

onUnmounted(() => {
  document.removeEventListener('click', handleClickOutside)
})

interface NavItem {
  key: string;
  url?: string;
  sections?: {
    key: string;
    links: {
      key: string;
      url: string;
    }[];
  }[];
}

const mainNavItems: NavItem[] = [
  {
    key: 'products',
    sections: [
      {
        key: 'solutions',
        links: [
          { key: 'platform', url: '/products/platform' },
          { key: 'constructionManagement', url: '/products/construction-management' },
          { key: 'buildingOperations', url: '/products/building-operations' }
        ]
      },
      {
        key: 'capabilities',
        links: [
          { key: 'plans', url: '/products/plans-bim' },
          { key: 'documentManagement', url: '/products/document-management' },
          { key: 'reporting', url: '/products/reporting' },
          { key: 'schedules', url: '/products/schedules' },
          { key: 'integrations', url: '/products/integrations' },
          { key: 'security', url: '/products/security' }
        ]
      },
      {
        key: 'visualDocumentation',
        links: [
          { key: 'siteView', url: '/products/site-view' }
        ]
      }
    ]
  },
  {
    key: 'customers',
    sections: [
      {
        key: 'overview',
        links: [
          { key: 'overview', url: '/customers' }
        ]
      },
      {
        key: 'segments',
        links: [
          { key: 'contractors', url: '/customers/contractors' },
          { key: 'developers', url: '/customers/developers' },
          { key: 'facilityManagers', url: '/customers/facility-managers' },
          { key: 'specialtyContractors', url: '/customers/specialty-contractors' },
          { key: 'consultancies', url: '/customers/consultancies' },
          { key: 'architects', url: '/customers/architects' }
        ]
      }
    ]
  },
  {
    key: 'industries',
    sections: [
      {
        key: 'solutions',
        links: [
          { key: 'construction', url: '/industries/construction' },
          { key: 'realEstate', url: '/industries/real-estate' },
          { key: 'facilityManagement', url: '/industries/facility-management' }
        ]
      },
      {
        key: 'sectors',
        links: [
          { key: 'retail', url: '/industries/retail' },
          { key: 'infrastructure', url: '/industries/infrastructure' },
          { key: 'hospitality', url: '/industries/hospitality' },
          { key: 'publicSector', url: '/industries/public-sector' },
          { key: 'office', url: '/industries/office' },
          { key: 'industrial', url: '/industries/industrial' },
          { key: 'residential', url: '/industries/residential' }
        ]
      }
    ]
  },
  {
    key: 'resources',
    sections: [
      {
        key: 'resources',
        links: [
          { key: 'quickStart', url: '/resources/quick-start' },
          { key: 'helpCenter', url: '/resources/help-center' },
          { key: 'blog', url: '/resources/blog' },
          { key: 'events', url: '/resources/events' },
          { key: 'security', url: '/resources/security' },
          { key: 'academy', url: '/resources/academy' }
        ]
      }
    ]
  },
  {
    key: 'about',
    sections: [
      {
        key: 'company',
        links: [
          { key: 'aboutUs', url: '/about/company' },
          { key: 'team', url: '/about/team' },
          { key: 'press', url: '/about/press' },
          { key: 'careers', url: '/about/careers' },
          { key: 'partners', url: '/about/partners' }
        ]
      }
    ]
  }
]

// 移动端菜单状态
const showMobileMenu = ref(false)
const mobileExpandedItems = ref<Record<number, boolean>>({})

// 切换移动端菜单
const toggleMobileMenu = () => {
  showMobileMenu.value = !showMobileMenu.value
}

// 切换移动端子菜单
const toggleMobileSubmenu = (index: number) => {
  mobileExpandedItems.value = {
    ...mobileExpandedItems.value,
    [index]: !mobileExpandedItems.value[index]
  }
}
</script>

<style scoped>
/* 添加这些样式到组件中 */
.nav-group {
  position: relative;
}

.nav-group:hover .dropdown-menu {
  display: block;
}

.dropdown-menu {
  display: none;
  position: absolute;
  left: 0;
  top: 100%;
  z-index: 50;
  min-width: 16rem;
}

/* 创建一个看不见的桥接元素，连接主菜单和子菜单 */
.dropdown-menu::before {
  content: '';
  position: absolute;
  top: -10px;
  left: 0;
  width: 100%;
  height: 10px;
  background: transparent;
}

/* 添加移动端菜单过渡效果 */
.mobile-menu-enter-active,
.mobile-menu-leave-active {
  transition: opacity 0.3s, transform 0.3s;
}

.mobile-menu-enter-from,
.mobile-menu-leave-to {
  opacity: 0;
  transform: translateY(-10px);
}
</style>