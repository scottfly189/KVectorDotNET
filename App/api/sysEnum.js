import { http } from 'uview-plus'

// 获取所有枚举类型
export const enumTypeListApi = (params) => http.get('/api/sysEnum/enumTypeList', params)

// 通过枚举类型获取枚举值集合
export const enumDataListApi = (params) => http.get('/api/sysEnum/enumDataList', params)

