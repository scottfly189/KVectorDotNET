import { http } from 'uview-plus'

// 获取系统更新日志分页列表
export const pageApi = (data) => http.post('/api/sysUpgrade/page', data)

// 增加系统更新日志
export const addApi = (data) => http.post('/api/sysUpgrade/add', data)

// 更新系统更新日志
export const updateApi = (data) => http.post('/api/sysUpgrade/update', data)

// 删除系统更新日志
export const deleteApi = (data) => http.post('/api/sysUpgrade/delete', data)

// 设置系统更新日志已读状态
export const setReadApi = (data) => http.post('/api/sysUpgrade/setRead', data)

// 获取最新的系统更新日志
export const lastUnReadApi = (params) => http.get('/api/sysUpgrade/lastUnRead', params)

