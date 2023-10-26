
#ifndef DOGTOOTH_NODE__DEVICES__MOTOR_POWER_HPP_
#define DOGTOOTH_NODE__DEVICES__MOTOR_POWER_HPP_

#include <memory>
#include <string>

#include <std_srvs/srv/set_bool.hpp>
#include "dogtooth_node/devices/devices.hpp"

namespace robotis
{
  namespace dogtooth
  {
    namespace devices
    {
      class MotorPower : public Devices
      {
        public:
          static void request(rclcpp::Client<std_srvs::srv::SetBool>::SharedPtr client, std_srvs::srv::SetBool::Request req);
          explicit MotorPower(std::shared_ptr<rclcpp::Node> & nh, const std::string & server_name = "motor_power");
          void command(const void * request, void * response) override;

        private:
          rclcpp::Service<std_srvs::srv::SetBool>::SharedPtr srv_;
      };
    }  // namespace devices
  }  // namespace dogtooth
}  // namespace robotis
#endif  // DOGTOOTH_NODE__DEVICES__MOTOR_POWER_HPP_
