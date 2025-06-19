import { http } from 'uview-plus'

// 获取异常日志分页列表
export const pageApi = (data) => http.post('/api/sysLogEx/page', data)

// 获取异常日志详情
export const detailApi = (params) => http.get('/api/sysLogEx/detail', params)

// 清空异常日志
export const clearApi = (data) => http.post('/api/sysLogEx/clear', data)

// 导出异常日志
export const exportApi = (data) => http.post('/api/sysLogEx/export', data)

