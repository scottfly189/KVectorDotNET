<template>
  <div>
    <!-- 启动加载页面 -->
    <Transition name="fade">
      <div v-if="isLoading" class="loading-screen">
        <div class="loading-content">
          <!-- <ConstructionLogo width="80" height="80" /> -->
          <div class="loader mt-10"></div>
          <h2 class="mt-10 text-xl font-bold text-gray-500">Loading...</h2>
        </div>
      </div>
    </Transition>

    <!-- 主应用内容 -->
    <NuxtLayout>
      <NuxtPage />
    </NuxtLayout>
  </div>
</template>

<script setup>
import { ref, onMounted, onBeforeUnmount } from 'vue'
import { useI18n } from 'vue-i18n'

const { locale } = useI18n()
const isLoading = ref(true)

// 在应用挂载时检查并应用保存的语言设置
onMounted(() => {
  // 从 localStorage 获取保存的语言
  const savedLocale = localStorage.getItem('user-locale')
  
  // 如果有保存的语言且与当前不同，则应用它
  if (savedLocale && savedLocale !== locale.value) {
    locale.value = savedLocale
  }

  isLoading.value = false;
})
</script>

<style>
/* 加载页面样式 */
.loading-screen {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: white;
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 9999;
}

.loading-content {
  display: flex;
  flex-direction: column;
  align-items: center;
}

.loading-spinner {
  margin-top: 20px;
  width: 40px;
  height: 40px;
  border: 4px solid rgba(0, 0, 0, 0.1);
  border-radius: 50%;
  border-top-color: #2563eb;
  animation: spin 1s ease-in-out infinite;
}

@keyframes spin {
  to { transform: rotate(360deg); }
}

/* 淡入淡出过渡效果 */
.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.5s ease;
}

.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}
/* HTML: <div class="loader"></div> */
.loader {
  width: 40px;
  height: 26px;
  --c:no-repeat linear-gradient(#000 0 0);
  background:
    var(--c) 0    100%,
    var(--c) 50%  100%,
    var(--c) 100% 100%;
  background-size:8px calc(100% - 4px);
  position: relative;
}
.loader:before {
  content: "";
  position: absolute;
  width: 8px;
  height: 8px;
  border-radius: 50%;
  background: #000;
  left: 0;
  top: 0;
  animation: 
    l3-1 1.5s  linear infinite alternate,
    l3-2 0.75s cubic-bezier(0,200,.8,200) infinite;
}
@keyframes l3-1 {
  100% {left:calc(100% - 8px)}
}
@keyframes l3-2 {
  100% {top:-0.1px}
}
</style>
