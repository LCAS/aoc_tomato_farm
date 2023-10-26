#ifndef DOGTOOTH_NODE__DEVICES__DEVICES_HPP_
#define DOGTOOTH_NODE__DEVICES__DEVICES_HPP_

#include <memory>
#include <string>
#include <utility>

#include <rclcpp/rclcpp.hpp>

namespace robotis
{
  namespace dogtooth
  {
    namespace devices
    {
      class Devices
      {
        public:
          explicit Devices(std::shared_ptr<rclcpp::Node> & nh): nh_(nh){

          }
          virtual void command(const void * request, void * response) = 0;

        protected:
          std::shared_ptr<rclcpp::Node> nh_;
          rclcpp::QoS qos_ = rclcpp::QoS(rclcpp::ServicesQoS());
      };
    }  // namespace devices
  }  // namespace dogtooth
}  // namespace robotis
#endif  // DOGTOOTH_NODE__DEVICES__DEVICES_HPP_
