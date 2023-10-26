#include "dogtooth_node/sensors/imu.hpp"
#include <memory>
#include <string>
#include <utility>

using robotis::dogtooth::sensors::Imu;

Imu::Imu(std::shared_ptr<rclcpp::Node> & nh, const std::string & imu_topic_name, const std::string & mag_topic_name,
  const std::string & frame_id): Sensors(nh, frame_id)
{
  imu_pub_ = nh->create_publisher<sensor_msgs::msg::Imu>(imu_topic_name, this->qos_);
  mag_pub_ = nh->create_publisher<sensor_msgs::msg::MagneticField>(mag_topic_name, this->qos_);
  RCLCPP_INFO(nh_->get_logger(), "Succeeded to create imu publisher");
}

void Imu::publish(const rclcpp::Time & now)
{
  auto imu_msg = std::make_unique<sensor_msgs::msg::Imu>();

  imu_msg->header.frame_id = this->frame_id_;
  imu_msg->header.stamp = now;
  // imu_msg->orientation.w = 
  // imu_msg->orientation.x = 
  // imu_msg->orientation.y = 
  // imu_msg->orientation.z = 
  // imu_msg->angular_velocity.x = 
  // imu_msg->angular_velocity.y = 
  // imu_msg->angular_velocity.z = 
  // imu_msg->linear_acceleration.x = 
  // imu_msg->linear_acceleration.y = 
  // imu_msg->linear_acceleration.z = 

  auto mag_msg = std::make_unique<sensor_msgs::msg::MagneticField>();
  mag_msg->header.frame_id = this->frame_id_;
  mag_msg->header.stamp = now;
  // mag_msg->magnetic_field.x = 
  // mag_msg->magnetic_field.y = 
  // mag_msg->magnetic_field.z = 

  imu_pub_->publish(std::move(imu_msg));
  mag_pub_->publish(std::move(mag_msg));
}
