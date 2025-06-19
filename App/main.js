import App from './App'
import uviewPlus from 'uview-plus'
import { initRequest } from './utils/request'

// #ifndef VUE3
import Vue from 'vue'
import './uni.promisify.adaptor'

Vue.config.productionTip = false
App.mpType = 'app'
const app = new Vue({
	...App
})
app.$mount()
// #endif

// #ifdef VUE3
import { createSSRApp } from 'vue'

export function createApp() {
	const app = createSSRApp(App)

	initRequest(app) // 引入请求封装
	app.use(uviewPlus) // 引入uviewPlus

	return { app }
}
// #endif