import { http } from 'uview-plus'

// 获取日程列表
export const pageApi = (data) => http.post('/api/sysSchedule/page', data)

// 获取日程详情
export const detailApi = (params) => http.get('/api/sysSchedule/detail', params)

// 增加日程
export const addApi = (data) => http.post('/api/sysSchedule/add', data)

// 更新日程
export const updateApi = (data) => http.post('/api/sysSchedule/update', data)

// 删除日程
export const deleteApi = (data) => http.post('/api/sysSchedule/delete', data)

// 设置日程状态
export const setStatusApi = (data) => http.post('/api/sysSchedule/setStatus', data)

