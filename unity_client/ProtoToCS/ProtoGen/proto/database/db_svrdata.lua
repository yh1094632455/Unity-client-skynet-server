package db;

import 'db_common.proto';

message one_monster_info
{
	optional int32 monster_id = 1[default = 0];
	optional int32 level = 2[default = 0];
	optional int32 curr_hp = 3[default = 0];
	optional int32 max_hp = 4[default = 0];
	
}

message one_guild_hurt
{
	optional uint32 gid = 1[default = 0];
	optional uint32 total_output = 2[default = 0];
}

message one_warlords_record
{
	optional int32 war_id = 1[default = 0];	    //战役ID
	repeated one_monster_info monster_list = 2;	//一场战斗有多个怪
	optional int32 is_die = 4[default = 0];	    //是否全部挂了
	optional int32 is_ranked = 5[default = 0];	//0 未排序 1 排序过    军团伤害的排序
	optional uint32 killer_uid = 6[default = 0]; //最后击杀者
	repeated one_guild_hurt guild_hurt_list = 7;//所有军团造成的伤害列表
	optional uint32 most_hurt_gid  = 9[default = 0];//伤害最高的军团
	optional uint32 seq = 10 [default = 0];     //信息序号
}

message dianjiang_info
{
	repeated int32 today_hotspot_xinwu_list = 1;
}

message one_camps_passed_info
{
	optional int32 copy_id = 1 [default = 0];
	optional uint32 uid = 2[default = 0];
	optional int32 passed_time = 3[default = 0];
	optional one_camps_def_info camps_def_info = 4; //闯连营阵容
}

message ext_param_msg
{
	optional string var_str = 1[default = ''];
	repeated one_warlords_record warlords_record_list = 2;
	optional dianjiang_info common_dianjiang_info = 3;
	repeated one_camps_passed_info camps_passed_info_list = 4;
}

message Svrdata
{
	optional uint32 uid = 1[default = 0];
	optional int32 int1 = 2[default = 0];
	optional int32 int2 = 3[default = 0];
	optional int32 int3 = 4[default = 0];
	optional ext_param_msg param_msg = 5;
}
