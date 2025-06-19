import { http } from 'uview-plus'

// 获取备份列表
export const listApi = (params) => http.get('/api/sysDbBackup/list', params)

// 备份数据库
export const addApi = (data) => http.post('/api/sysDbBackup/add', data)

// 删除备份
export const deleteApi = (data) => http.post('/api/sysDbBackup/delete', data)

