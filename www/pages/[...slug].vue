<template>
  <div class="flex flex-col items-center justify-center min-h-screen p-4">
    <svg class="w-64 h-64 mb-6" viewBox="0 0 200 200" xmlns="http://www.w3.org/2000/svg">
      <!-- 背景圆 -->
      <circle cx="100" cy="100" r="90" fill="#f3f4f6" />
      
      <!-- 404文字 -->
      <text x="50" y="120" font-family="Arial" font-size="60" font-weight="bold" fill="#6b7280">
        404
      </text>
      
      <!-- 困惑表情 -->
      <circle cx="70" cy="60" r="8" fill="#6b7280" /> <!-- 左眼 -->
      <circle cx="130" cy="60" r="8" fill="#6b7280" /> <!-- 右眼 -->
      
      <!-- 困惑的嘴 -->
      <path d="M60,140 Q100,120 140,140" stroke="#6b7280" stroke-width="5" fill="none" />
      
      <!-- 问号 -->
      <text x="90" y="90" font-family="Arial" font-size="30" font-weight="bold" fill="#6b7280">?</text>
    </svg>
    
    <h1 class="text-2xl font-bold text-gray-800">404 - Page Not Found</h1>
  </div>
</template>

