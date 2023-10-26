#ifndef DOGTOOTH_NODE__SENSORS__SENSORS_HPP_
#define DOGTOOTH_NODE__SENSORS__SENSORS_HPP_

#include <memory>
#include <string>
#include <utility>

#include <rclcpp/rclcpp.hpp>


namespace robotis
{
  namespace dogtooth
  {
    namespace sensors
    {
      class Sensors
      {
        public:
          explicit Sensors(
            std::shared_ptr<rclcpp::Node> & nh,
            const std::string & frame_id = ""): nh_(nh),
            frame_id_(frame_id){}

          virtual void publish(const rclcpp::Time & now) = 0;

        protected:
          std::shared_ptr<rclcpp::Node> nh_;
          std::string frame_id_;
          rclcpp::QoS qos_ = rclcpp::QoS(rclcpp::KeepLast(10));
      };
    }  // namespace sensors
  }  // namespace dogtooth
}  // namespace robotis
#endif  // DOGTOOTH_NODE__SENSORS__SENSORS_HPP_
