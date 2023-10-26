#include "dogtooth_node/sensors/sensor_state.hpp"

#include <memory>
#include <string>
#include <utility>

using robotis::dogtooth::sensors::SensorState;

SensorState::SensorState(
  std::shared_ptr<rclcpp::Node> & nh,
  const std::string & topic_name, const uint8_t & bumper_forward, const uint8_t & bumper_backward)
        : Sensors(nh), bumper_forward_(bumper_forward), bumper_backward_(bumper_backward){

  pub_ = nh->create_publisher<dogtooth_msgs ::msg::SensorState>(topic_name, this->qos_);
  RCLCPP_INFO(nh_->get_logger(), "Succeeded to create sensor state publisher");

}

void SensorState::publish(const rclcpp::Time & now)
{
  auto msg = std::make_unique<dogtooth_msgs::msg::SensorState>();
  msg->header.stamp = now;
  //TODO  
  pub_->publish(std::move(msg));
}
