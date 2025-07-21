/// 系统用户表
class SysUser {
  /// 账号
  final String account;

  /// 密码
  final String? password;

  /// 真实姓名
  final String? realName;

  /// 昵称
  final String? nickName;

  /// 头像
  final String? avatar;

  /// 性别-男_1、女_2
  final int sex;

  /// 年龄
  final int age;

  /// 出生日期
  final DateTime? birthday;

  /// 民族
  final String? nation;

  /// 手机号码
  final String? phone;

  /// 证件类型
  final int cardType;

  /// 身份证号
  final String? idCardNum;

  /// 邮箱
  final String? email;

  /// 教学导航
  final bool isTour;

  /// 地址
  final String? address;

  /// 文化程度
  final int cultureLevel;

  /// 政治面貌
  final String? politicalOutlook;

  /// 毕业院校
  final String? college;

  /// 办公电话
  final String? officePhone;

  /// 紧急联系人
  final String? emergencyContact;

  /// 紧急联系人电话
  final String? emergencyPhone;

  /// 紧急联系人地址
  final String? emergencyAddress;

  /// 个人简介
  final String? introduction;

  /// 排序
  final int orderNo;

  /// 状态
  final int status;

  /// 备注
  final String? remark;

  /// 账号类型
  final int accountType;

  /// 直属机构Id
  final int orgId;

  /// 直属主管Id
  final int? managerUserId;

  /// 职位Id
  final int posId;

  /// 工号
  final String? jobNum;

  /// 职级
  final String? posLevel;

  /// 职称
  final String? posTitle;

  /// 擅长领域
  final String? expertise;

  /// 办公区域
  final String? officeZone;

  /// 办公室
  final String? office;

  /// 入职日期
  final DateTime? joinDate;

  /// 最新登录Ip
  final String? lastLoginIp;

  /// 最新登录地点
  final String? lastLoginAddress;

  /// 最新登录时间
  final DateTime? lastLoginTime;

  /// 最新登录设备
  final String? lastLoginDevice;

  /// 电子签名
  final String? signature;

  /// Token版本号
  final int tokenVersion;

  /// 最新密码修改时间
  final DateTime? lastChangePasswordTime;

  SysUser({
    required this.account,
    this.password,
    this.realName,
    this.nickName,
    this.avatar,
    this.sex = 1,
    this.age = 0,
    this.birthday,
    this.nation,
    this.phone,
    this.cardType = 0,
    this.idCardNum,
    this.email,
    this.isTour = false,
    this.address,
    this.cultureLevel = 10,
    this.politicalOutlook,
    this.college,
    this.officePhone,
    this.emergencyContact,
    this.emergencyPhone,
    this.emergencyAddress,
    this.introduction,
    this.orderNo = 100,
    this.status = 1,
    this.remark,
    this.accountType = 777,
    required this.orgId,
    this.managerUserId,
    required this.posId,
    this.jobNum,
    this.posLevel,
    this.posTitle,
    this.expertise,
    this.officeZone,
    this.office,
    this.joinDate,
    this.lastLoginIp,
    this.lastLoginAddress,
    this.lastLoginTime,
    this.lastLoginDevice,
    this.signature,
    this.tokenVersion = 1,
    this.lastChangePasswordTime,
  });
}
