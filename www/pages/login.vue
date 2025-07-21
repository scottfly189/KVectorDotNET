<script setup lang="ts">
//@ts-ignore
definePageMeta({
  layout: 'register-layout',
})

// 导入 ElMessage 和图标
import { ElMessage } from 'element-plus'
import { EditPen as ElIconEditPen } from '@element-plus/icons-vue'

// 创建消息提示函数
const showMessage = () => {
  ElMessage('hello')
}
</script>

<template>
  <div class="login-page">
    <div class="login-container">
      <h1>登录页面</h1>
    </div>
    <div>
      <el-button @click="showMessage">button</el-button>
      <ElButton :icon="ElIconEditPen" type="success">button</ElButton>
      <LazyElButton type="warning">lazy button</LazyElButton>
    </div>
  </div>

</template>
