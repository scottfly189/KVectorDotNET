import { http } from 'uview-plus'

// 获取操作日志分页列表
export const pageApi = (data) => http.post('/api/sysLogOp/page', data)

// 获取操作日志详情
export const detailApi = (params) => http.get('/api/sysLogOp/detail', params)

// 清空操作日志
export const clearApi = (data) => http.post('/api/sysLogOp/clear', data)

// 导出操作日志
export const exportApi = (data) => http.post('/api/sysLogOp/export', data)

// 按年按天数统计消息日志
export const yearDayStatsApi = (params) => http.get('/api/sysLogOp/yearDayStats', params)

