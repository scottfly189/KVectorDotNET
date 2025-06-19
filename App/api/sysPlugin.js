import { http } from 'uview-plus'

// 获取动态插件列表
export const pageApi = (data) => http.post('/api/sysPlugin/page', data)

// 增加动态插件
export const addApi = (data) => http.post('/api/sysPlugin/add', data)

// 更新动态插件
export const updateApi = (data) => http.post('/api/sysPlugin/update', data)

// 删除动态插件
export const deleteApi = (data) => http.post('/api/sysPlugin/delete', data)

// 添加动态程序集/接口
export const compileAssemblyApi = (data) => http.post('/api/sysPlugin/compileAssembly', data)

// 移除动态程序集/接口
export const removeAssemblyApi = (data) => http.post('/api/sysPlugin/removeAssembly', data)

