<script setup lang="ts">
import { useI18n } from 'vue-i18n'

const { t } = useI18n()
</script>

<template>
  <div>
    <!-- Hero Section -->
    <section class="py-20 bg-gradient-to-r from-blue-50 to-indigo-50">
      <div class="container mx-auto px-4">
        <div class="max-w-3xl mx-auto text-center">
          <h1 class="text-4xl md:text-5xl font-bold mb-6">
            {{ t('home.title') }}
          </h1>
          <p class="text-xl text-gray-600 mb-8">
            {{ t('home.subtitle') }}
          </p>
          <div class="flex justify-center">
            <NuxtLink to="/register" class="bg-yellow-500 text-white px-6 py-3 rounded-md hover:bg-yellow-600">
              {{ t('header.trial') }}
            </NuxtLink>
          </div>
        </div>
      </div>
    </section>

    <!-- Features Section -->
    <section class="py-16 bg-white">
      <div class="container mx-auto px-4">
        <h2 class="text-3xl font-bold text-center mb-12">
          {{ t('home.coreFeatures') }}
        </h2>
        <div class="grid md:grid-cols-3 gap-8">
          <div class="p-6 border rounded-lg">
            <h3 class="text-xl font-bold mb-3">{{ t('home.defectManagement.title') }}</h3>
            <p class="text-gray-600">{{ t('home.defectManagement.description') }}</p>
          </div>
          <div class="p-6 border rounded-lg">
            <h3 class="text-xl font-bold mb-3">{{ t('home.documentManagement.title') }}</h3>
            <p class="text-gray-600">{{ t('home.documentManagement.description') }}</p>
          </div>
          <div class="p-6 border rounded-lg">
            <h3 class="text-xl font-bold mb-3">{{ t('home.dataReporting.title') }}</h3>
            <p class="text-gray-600">{{ t('home.dataReporting.description') }}</p>
          </div>
        </div>
        <div class="grid md:grid-cols-3 gap-8 mt-5">
          <div class="p-6 border rounded-lg">
            <h3 class="text-xl font-bold mb-3">{{ t('home.projectManagement.title') }}</h3>
            <p class="text-gray-600">{{ t('home.projectManagement.description') }}</p>
          </div>
          <div class="p-6 border rounded-lg">
            <h3 class="text-xl font-bold mb-3">{{ t('home.teamCollaboration.title') }}</h3>
            <p class="text-gray-600">{{ t('home.teamCollaboration.description') }}</p>
          </div>
          <div class="p-6 border rounded-lg">
            <h3 class="text-xl font-bold mb-3">{{ t('home.mobileAccess.title') }}</h3>
            <p class="text-gray-600">{{ t('home.mobileAccess.description') }}</p>
          </div>
        </div>
      </div>
    </section>
  </div>
</template> 