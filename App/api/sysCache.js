import { http } from 'uview-plus'

// 申请分布式锁
export const beginCacheLockApi = (data) => http.post('/api/sysCache/beginCacheLock', data)

// 获取缓存键名集合
export const keyListApi = (params) => http.get('/api/sysCache/keyList', params)

// 删除缓存
export const deleteApi = (data) => http.post('/api/sysCache/delete', data)

// 清空所有缓存
export const clearApi = (data) => http.post('/api/sysCache/clear', data)

// 根据键名前缀删除缓存
export const deleteByPreKeyApi = (data) => http.post('/api/sysCache/deleteByPreKey', data)

// 根据键名前缀获取键名集合
export const keysByPrefixKeyApi = (params) => http.get('/api/sysCache/keysByPrefixKey', params)

// 获取缓存值
export const valueApi = (params) => http.get('/api/sysCache/value', params)

