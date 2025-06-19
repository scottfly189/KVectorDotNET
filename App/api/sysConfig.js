import { http } from 'uview-plus'

// 获取配置参数分页列表
export const pageApi = (data) => http.post('/api/sysConfig/page', data)

// 获取配置参数列表
export const listApi = (data) => http.post('/api/sysConfig/list', data)

// 增加配置参数
export const addApi = (data) => http.post('/api/sysConfig/add', data)

// 更新配置参数
export const updateApi = (data) => http.post('/api/sysConfig/update', data)

// 更新参数默认值
export const updateDefaultApi = (data) => http.post('/api/sysConfig/updateDefault', data)

// 删除配置参数
export const deleteApi = (data) => http.post('/api/sysConfig/delete', data)

// 批量删除配置参数
export const batchDeleteApi = (data) => http.post('/api/sysConfig/batchDelete', data)

// 获取配置参数详情
export const detailApi = (params) => http.get('/api/sysConfig/detail', params)

// 根据Code获取配置参数值
export const configValueByCodeApi = (params) => http.get('/api/sysConfig/configValueByCode', params)

// 获取分组列表
export const groupListApi = (params) => http.get('/api/sysConfig/groupList', params)

// 批量更新配置参数值
export const batchUpdateApi = (data) => http.post('/api/sysConfig/batchUpdate', data)

