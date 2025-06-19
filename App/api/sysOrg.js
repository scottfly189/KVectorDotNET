import { http } from 'uview-plus'

// 获取机构列表
export const listApi = (params) => http.get('/api/sysOrg/list', params)

// 增加机构
export const addApi = (data) => http.post('/api/sysOrg/add', data)

// 更新机构
export const updateApi = (data) => http.post('/api/sysOrg/update', data)

// 删除机构
export const deleteApi = (data) => http.post('/api/sysOrg/delete', data)

