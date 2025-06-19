import { http } from 'uview-plus'

// 获取服务器配置信息
export const serverBaseApi = (params) => http.get('/api/sysServer/serverBase', params)

// 获取服务器使用信息
export const serverUsedApi = (params) => http.get('/api/sysServer/serverUsed', params)

// 获取服务器磁盘信息
export const serverDiskApi = (params) => http.get('/api/sysServer/serverDisk', params)

// 获取框架主要程序集
export const assemblyListApi = (params) => http.get('/api/sysServer/assemblyList', params)

