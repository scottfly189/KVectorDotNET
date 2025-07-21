<template>
  <img :src="logoSrc" :width="width" :height="height" alt="Construction Logo" class="construction-logo" />
</template>

<script setup>
const logoSrc = ref('/images/logo.png')

defineProps({
  width: {
    type: [Number, String],
    default: 24
  },
  height: {
    type: [Number, String],
    default: 24
  },
  primaryColor: {
    type: String,
    default: '#2563eb' // 蓝色
  },
  secondaryColor: {
    type: String,
    default: '#f59e0b' // 黄色
  },
  accentColor: {
    type: String,
    default: '#10b981' // 绿色
  }
})
</script>

<style scoped>
.construction-logo {
  transition: all 0.3s ease;
}
.construction-logo:hover {
  filter: drop-shadow(0 0 3px rgba(37, 99, 235, 0.5));
}
</style> 