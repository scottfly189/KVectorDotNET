import { http } from 'uview-plus'

// 获取系统域登录配置分页列表
export const pageApi = (data) => http.post('/api/sysLdap/page', data)

// 增加系统域登录配置
export const addApi = (data) => http.post('/api/sysLdap/add', data)

// 更新系统域登录配置
export const updateApi = (data) => http.post('/api/sysLdap/update', data)

// 删除系统域登录配置
export const deleteApi = (data) => http.post('/api/sysLdap/delete', data)

// 获取系统域登录配置详情
export const detailApi = (params) => http.get('/api/sysLdap/detail', params)

// 获取系统域登录配置列表
export const listApi = (params) => http.get('/api/sysLdap/list', params)

// 同步域用户
export const syncUserApi = (data) => http.post('/api/sysLdap/syncUser', data)

// 同步域组织
export const syncDeptApi = (data) => http.post('/api/sysLdap/syncDept', data)

