import { http } from 'uview-plus'

// 获取差异日志分页列表
export const pageApi = (data) => http.post('/api/sysLogDiff/page', data)

// 获取差异日志详情
export const detailApi = (params) => http.get('/api/sysLogDiff/detail', params)

