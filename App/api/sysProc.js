import { http } from 'uview-plus'

// 导出存储过程数据
export const pocExport2Api = (data) => http.post('/api/sysProc/pocExport2', data)

// 根据模板导出存储过程数据
export const pocExportApi = (data) => http.post('/api/sysProc/pocExport', data)

// 获取存储过程返回表
export const procTableApi = (data) => http.post('/api/sysProc/procTable', data)

// 获取存储过程返回数据集
export const commonDataSetApi = (data) => http.post('/api/sysProc/commonDataSet', data)

