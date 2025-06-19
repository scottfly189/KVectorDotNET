import { http } from 'uview-plus'

// 获取职位分页列表
export const pageApi = (data) => http.post('/api/sysPos/page', data)

// 获取职位列表
export const listApi = (params) => http.get('/api/sysPos/list', params)

// 增加职位
export const addApi = (data) => http.post('/api/sysPos/add', data)

// 更新职位
export const updateApi = (data) => http.post('/api/sysPos/update', data)

// 删除职位
export const deleteApi = (data) => http.post('/api/sysPos/delete', data)

